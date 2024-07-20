using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Shared.Cryptography;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.AI.Fsm;
using WorldServer.Game.AI.Fsm.Conditions;
using WorldServer.Game.AI.Fsm.States;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Manager;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public class BNpc : Character
{
    public uint InstanceId;
    public uint BNpcBaseId;
    public uint BNpcNameId;
    public ulong WeaponMain;
    public uint TriggerOwnerId;
    public ulong WeaponSub;
    public byte AggressionMode = 2;
    public byte EnemyType = 4;
    public ushort ModelChara;
    public uint DisplayFlags;
    public uint GimmickId;
    public byte[] Customize = new byte[26];
    public uint[] ModelEquip = new uint[10];
    public StateMachine StateMachine;

    public Vector3 SpawnPosition;
    public Vector3 RoamPosition;

    public bool RoamTargetReached = false;
    public long LastRoamReachedTime;
    public List<HateListEntry> HateList = [];
    
    public BNpc(WorldPosition position)
        : base(XxHash.CalculateHash(Encoding.UTF8.GetBytes($"{AssetManager.NextBNpcId}::BNPC")), ActorType.Npc)
    {
        Position = position;
        this.SpawnPosition = position.Offset;
        this.StateMachine = new StateMachine(this);
        var stateIdle = new StateIdle();
        var stateRoam = new StateRoam();
        var stateDead = new StateDead();

        /*
            this.StateMachine.AddState(stateRoam);
            stateIdle.AddTransition(stateRoam, new RoamNextTimeReachedCondition());
        */
        stateRoam.AddTransition(stateIdle, new RoamTargetReachedCondition());
        stateRoam.AddTransition(stateDead, new IsDeadCondition());
            
        stateIdle.AddTransition(stateDead, new IsDeadCondition());
        this.StateMachine.AddState(stateIdle);
        this.StateMachine.SetCurrentState(stateIdle);
    }
    public void InitFromTemplate(BNpcTemplate template)
    {
        InstanceId = template.instanceId;
        BNpcBaseId = template.BaseId;
        BNpcNameId = template.NameId;
        AggressionMode = template.ActiveType;
        this._level = (byte)template.Level;
        Position.Relocate(new Vector3((float)template.x, (float)template.y, (float)template.z), (float)template.Rotation);
        this.GimmickId = template.BoundInstanceID;

        var bnpcBase = GameTableManager.BNpcBase.GetRow(this.BNpcBaseId);
        if (bnpcBase is not null)
        {
            EnemyType = bnpcBase.Battalion;
            Radius = bnpcBase.Scale;
            if (bnpcBase.ModelChara.Value != null)
            {
                ModelChara = (ushort)bnpcBase.ModelChara.Row;
            }
            if (bnpcBase.NpcEquip.Value is not null)
            {
                var equipment = bnpcBase.NpcEquip.Value;
                this.WeaponMain = equipment.ModelMainHand;
                this.WeaponSub = equipment.ModelOffHand;
                this.ModelEquip =
                [
                    equipment.ModelHead, equipment.ModelBody, equipment.ModelHands, equipment.ModelLegs, equipment.ModelFeet, equipment.ModelEars,
                    equipment.ModelNeck, equipment.ModelWrists, equipment.ModelLeftRing, equipment.ModelRightRing
                ];
            }

            if (bnpcBase.BNpcCustomize.Value is not null)
            {
                var bNpcCustom = bnpcBase.BNpcCustomize.Value;
                this.Customize =
                [
                    (byte)bNpcCustom.Race.Row, bNpcCustom.Gender, bNpcCustom.BodyType, bNpcCustom.Height, (byte)bNpcCustom.Tribe.Row, bNpcCustom.Face,
                    bNpcCustom.HairColor, bNpcCustom.HairHighlight, bNpcCustom.SkinColor, bNpcCustom.EyeHeterochromia, bNpcCustom.HairColor,
                    bNpcCustom.HairHighlightColor, bNpcCustom.FacialFeature, bNpcCustom.FacialFeatureColor, bNpcCustom.Eyebrows, bNpcCustom.EyeColor,
                    bNpcCustom.EyeShape, bNpcCustom.Nose, bNpcCustom.Jaw, bNpcCustom.Mouth, bNpcCustom.LipColor, bNpcCustom.BustOrTone1,
                    bNpcCustom.ExtraFeature1, bNpcCustom.ExtraFeature2OrBust, bNpcCustom.FacePaint, bNpcCustom.FacePaintColor
                ];
            }
        }

        if (this.BNpcNameId == 541)
            this.InvincibilityType = InvincibilityType.InvincibilityRefill;
    }

    public void InitFromLevel(uint levelId, ushort nameId = 0)
    {
        var level = GameTableManager.Level.GetRow(levelId);
        if (level == null || level.Type != 9)
            return;

        InstanceId = levelId;
        BNpcBaseId = level.Object;
        this.BNpcNameId = nameId;
        Position.Relocate(new Vector3(level.X, level.Y, level.Z), level.Yaw);

        var bnpcBase = GameTableManager.BNpcBase.GetRow(this.BNpcBaseId);
        if (bnpcBase is not null)
        {
            EnemyType = bnpcBase.Battalion;
            Radius = bnpcBase.Scale;
            if (bnpcBase.ModelChara.Value != null)
            {
                ModelChara = (ushort)bnpcBase.ModelChara.Row;
            }
            if (bnpcBase.NpcEquip.Value is not null)
            {
                var equipment = bnpcBase.NpcEquip.Value;
                this.WeaponMain = equipment.ModelMainHand;
                this.WeaponSub = equipment.ModelOffHand;
                this.ModelEquip =
                [
                    equipment.ModelHead, equipment.ModelBody, equipment.ModelHands, equipment.ModelLegs, equipment.ModelFeet, equipment.ModelEars,
                    equipment.ModelNeck, equipment.ModelWrists, equipment.ModelLeftRing, equipment.ModelRightRing
                ];
            }

            if (bnpcBase.BNpcCustomize.Value is not null)
            {
                var bNpcCustom = bnpcBase.BNpcCustomize.Value;
                this.Customize =
                [
                    (byte)bNpcCustom.Race.Row, bNpcCustom.Gender, bNpcCustom.BodyType, bNpcCustom.Height, (byte)bNpcCustom.Tribe.Row, bNpcCustom.Face,
                    bNpcCustom.HairColor, bNpcCustom.HairHighlight, bNpcCustom.SkinColor, bNpcCustom.EyeHeterochromia, bNpcCustom.HairColor,
                    bNpcCustom.HairHighlightColor, bNpcCustom.FacialFeature, bNpcCustom.FacialFeatureColor, bNpcCustom.Eyebrows, bNpcCustom.EyeColor,
                    bNpcCustom.EyeShape, bNpcCustom.Nose, bNpcCustom.Jaw, bNpcCustom.Mouth, bNpcCustom.LipColor, bNpcCustom.BustOrTone1,
                    bNpcCustom.ExtraFeature1, bNpcCustom.ExtraFeature2OrBust, bNpcCustom.FacePaint, bNpcCustom.FacePaintColor
                ];
            }
        }
    }

    public override void Die()
    {
        base.Die();
        foreach (var entry in this.HateList)
        {
            var character = entry.Character;
            if (character.IsPlayer)
            {
                var player = character.ToPlayer;
                player.OnMobKill(this);
            }
        }

        if (Map.GetType() == typeof(InstanceContent))
        {
            var instance = (InstanceContent)Map;
            instance.Script?.OnBNpcKill(this);
        }
        Task.Delay(10000).ContinueWith(_ => RemoveFromMap());
        HateListClear();
    }

    public override void Update(double lastTick)
    {

        base.Update(lastTick);
        this.StateMachine.Update(lastTick);

    }

    public bool MoveTo(Vector3 pos)
    {
        var navigation = Map.NavigationProvider;
        if (navigation is null) return false;

        var agent = this.CrowdAgent;
        var pos1 = new Vector3(agent.npos.X, agent.npos.Y, agent.npos.Z);

        if (Vector3.Distance(pos1, pos) < this.Radius + 3f)
        {
            Face(pos);
            Relocate(new WorldPosition((ushort)Map.Entry.RowId, pos1, Position.Orientation));
            SendPositionUpdate();
            navigation.UpdateAgentPosition(this);
            return true;
        }

        Face(pos);
        Relocate(new WorldPosition((ushort)Map.Entry.RowId, pos1, Position.Orientation));
        SendPositionUpdate();
        return false;
    }

    public void Aggro(Character character)
    {
        SetStance(Stance.Active);
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.SetBattle,
            Parameter1 = 1,
        });
        HateListUpdate(character, 1);
        SetTarget(character.Id);
        if (character.IsPlayer)
        {
            var player = character.ToPlayer;
            player.OnMobAggro(this);
        }
    }

    public void HateListUpdate(Character character, uint hateAmount)
    {
        foreach (var entry in this.HateList)
        {
            if (entry.Character == character)
            {
                entry.HateAmount += hateAmount;
                return;
            }
                    
        }

        var hateEntry = new HateListEntry
        {
            Character = character,
            HateAmount = hateAmount
        };
        this.HateList.Add(hateEntry);

    }

    public void DeAggro(Character character)
    {
        this.HateList.RemoveAll(e => e.Character == character);


        if (character.IsPlayer)
        {
            var player = character.ToPlayer;
            player.OnMobDeAggro(this);
        }
    }

    public void HateListClear()
    {
        foreach (var entry in this.HateList.ToList())
        {
            DeAggro(entry.Character);
        }
        this.HateList.Clear();
    }

    public override void OnActionHostile(Character source)
    {
        Aggro(source);
    }
}