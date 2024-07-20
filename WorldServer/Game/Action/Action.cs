using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Action.Effects;
using WorldServer.Game.Action.Enums;
using WorldServer.Game.Calc;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network;
using WorldServer.Network.Message;
using WorldServer.Script;

namespace WorldServer.Game.Action;

public class Action
{
    public Lumina.Excel.GeneratedSheets.Action ActionData;
    public ActionData ServerActionData;
        
    public uint ActionId;
    public uint AdditionalId;
    public ulong StartTime;
    public uint CastTimeMs;
    public uint RecastTimeMs;

    public SkillType Type;
    public CastType CastType => (CastType)ActionData.CastType;
    public Character Source;
    public Character Target;
    public ulong TargetId;
    public WorldPosition Position;
    public List<Character> AffectedActors = new();
    public ActionScript Script { get; }

    public Dictionary<uint, List<Effect>> Effects = new();


    private ActionInterruptType InterruptType;
    public ActionPrimaryCostType PrimaryCostType;


        
    public Action(Character caster, uint actionId, ushort sequence, ulong targetId, WorldPosition sourcePosition, SkillType skillType, uint additionalId)
    {

        ActionData = GameTableManager.Action.GetRow(actionId);
        this.ServerActionData = DataManager.GetAction(actionId);
        this.ActionId = actionId;
        this.Source = caster;
        this.CastTimeMs = (uint)(ActionData.Cast100ms * 100);
        this.RecastTimeMs = (uint)(ActionData.Recast100ms * 100);
        this.TargetId = targetId;
        this.Target = MapManager.FindPlayer((uint)targetId);
        this.Position = sourcePosition;
        Script    = ScriptManager.NewActionScript(actionId);
        this.Type = skillType;
        this.AdditionalId = additionalId == 0 ? actionId : additionalId;
        this.PrimaryCostType = (ActionPrimaryCostType)this.ActionData.PrimaryCostType;
    }

    public bool HasCastTime()
    {
        return this.CastTimeMs > 0;
    }

    public bool PrimaryCostCheck(bool subtract)
    {
        switch (this.PrimaryCostType)
        {
            case ActionPrimaryCostType.MagicPoints:
            {
                var mpcost = (ushort)(this.ActionData.PrimaryCostValue * 100);
                if (this.Source.ToPlayer.MP > mpcost)
                {
                    if(subtract)
                        this.Source.ToPlayer.DeductMP(mpcost);
                    return true;
                }

                return false;

            }
            case ActionPrimaryCostType.Eukrasia:
            {
                var mpcost = (ushort)(this.ActionData.PrimaryCostValue * 100);
                if (this.Source.ToPlayer.HasStatusEffect(2606) && this.Source.ToPlayer.MP > mpcost)
                {
                    if (subtract)
                    {
                        this.Source.ToPlayer.RemoveStatusEffect(2606);
                        this.Source.ToPlayer.DeductMP(mpcost);
                    }

                    return true;
                }

                return false;
                    
            }
            case ActionPrimaryCostType.StatusEffect:
            {
                var statusId = this.ActionData.PrimaryCostValue;
                if (this.Source.ToPlayer.HasStatusEffect(statusId))
                {
                    this.Source.RemoveStatusEffect(this.ActionData.PrimaryCostValue);
                    return true;
                }

                return false;
            }
            case ActionPrimaryCostType.None:
            {
                return true;
            }
                
            default:
                return true;

        }
            
    }
        
    public bool HasClientsideTarget()
    {
        return TargetId > 0xFFFFFFFF;
    }
    public virtual void Start()
    {
        StartTime = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (this.Source.IsPlayer)
        {
            var player = this.Source.ToPlayer;
            player.Session.Send(new ServerActorActionSelf
            {
                Action = ActorActionServer.ActionStart,
                Parameter2 = this.ActionId,
                Parameter3 = this.RecastTimeMs / 10

            }); 
                    
        }
        if (HasCastTime())
        {

            this.Source.SendMessageToVisible(new ServerActorCast
            {
                Action = this
            }, true);
               
            if (this.Source.IsPlayer)
            {
                var player = this.Source.ToPlayer;
                player.SetStateFlag(PlayerStateFlag.Casting);
                    
            }
               
        }

        Script?.OnStart(this);
        // instantly finish cast if there's no cast time
        if( !HasCastTime() )
            Execute();
    }

    public bool ConsumeResources()
    {
        return PrimaryCostCheck(true);
    }
    public virtual void Execute()
    {
        if( !ConsumeResources() )
        {
            Console.WriteLine($"No resources: interrupting action {this.ActionId} {this.ActionData.Name} {this.PrimaryCostType.ToString()}");
            Interrupt();
            return;
        }
            
        if (HasCastTime())
        {
            if (this.Source.IsPlayer)
            {
                var player = this.Source.ToPlayer;
                player.UnsetStateFlag(PlayerStateFlag.Casting);
            }
        }
            
        if( !HasClientsideTarget()  )
        {
            BuildEffects();
        }

        if (!this.ActionData.PreservesCombo)
        {
            if (this.AffectedActors.Any() && (!IsCombo() || IsCorrectCombo()))
            {
                this.Source.LastComboActionId = this.ActionId;
            }
            else
            {
                this.Source.LastComboActionId = 0;
            }
        }
    }

    public bool isInterrupted()
    {
        return this.InterruptType != ActionInterruptType.None;
    }
            
    public void Interrupt(ActionInterruptType type = ActionInterruptType.RegularInterrupt)
    {
        if (isInterrupted())
            return;

        this.InterruptType = type;
            
        if (this.Source.IsPlayer)
        {
            var player = this.Source.ToPlayer;
            player.UnsetStateFlag(PlayerStateFlag.Casting);
        }
            
        if (this.StartTime > 0 && HasCastTime())
        {
            byte effect = 0;
            if (this.InterruptType == ActionInterruptType.DamageInterrupt)
                effect = 1;
                
            this.Source.SendMessageToVisible(new ServerActorAction
            {
                Action = ActorActionServer.CastInterrupt,
                Parameter1 = 0x219,
                Parameter2 = 1,
                Parameter3 = this.ActionId,
                Parameter4 = effect
            }, true);
        }
        Script?.OnInterrupt(this);
    }

    public void AddEffect(Character target, Effect effect)
    {
        var id = target.Id;
        if (!Effects.ContainsKey(id))
            Effects.Add(id, new List<Effect>());

        this.Effects[id].Add(effect);

    }
        
    public void BuildEffects()
    {
        SnapshotAffectedActors();
        Script?.OnExecute(this);


        foreach (var character in this.AffectedActors)
        {
            if (this.ServerActionData is not null)
            {
                Console.WriteLine($"Adding ServerActionData effects for action {this.ActionId}");
                if (this.ServerActionData.Potency.Heal > 0)
                    AddEffect(character, EffectBuilder.Heal(this, CalcHeal(this.ServerActionData.Potency.Heal), character));

                if (this.ServerActionData.Potency.Base > 0 && character != this.Source)
                {
                    Console.WriteLine(
                        $"Dealing damage with potency {(IsCorrectCombo() ? this.ServerActionData.Potency.Combo : this.ServerActionData.Potency.Base)}");
                    AddEffect(character, EffectBuilder.Damage(this, CalcDamage(IsCorrectCombo() ? this.ServerActionData.Potency.Combo : this.ServerActionData.Potency.Base), character));
                }

                foreach (var statusdata in this.ServerActionData.Statuses)
                {
                    var requiresCombo = (bool)(statusdata.Flags.GetValueOrDefault("Combo") ?? false);
                    var applyToSelf = (bool)(statusdata.Flags.GetValueOrDefault("ApplyToSelf") ?? false);
                    var onSource = (bool)(statusdata.Flags.GetValueOrDefault("OnSource") ?? false);
                    if (!applyToSelf && this.Source == character)
                        continue;
                    if (requiresCombo && !IsCorrectCombo())
                        continue;
                    if(onSource)
                        AddEffect(character, EffectBuilder.StatusEffectSource(this, (ushort)statusdata.Id, statusdata.Duration/1000, (ushort)statusdata.Param));
                    else
                        AddEffect(character, EffectBuilder.StatusEffectTarget(this, (ushort)statusdata.Id, statusdata.Duration/1000, (ushort)statusdata.Param, character));
                }

                if (!IsCombo() || IsCorrectCombo())
                {
                    if(!this.ActionData.PreservesCombo)
                        AddEffect(character, EffectBuilder.StartCombo(this, character));
                }

                /* Retail doesn't send that one anymore?
                if (IsCorrectCombo())
                {
                    AddEffect(character, EffectBuilder.ComboSucceed(this, character));
                }
                */

            }
            else
            {
                Console.WriteLine($"Action {this.ActionId} has no assigned ServerActionData!");
            }
        }
            
        ExecuteAndSendEffects();
    }


    public (float, ActionHitSeverityType) CalcDamage(uint potency)
    {
        return CalcManager.ActionDamage(this, this.Source, potency);
    }
        
    public (float, ActionHitSeverityType) CalcHeal(uint potency)
    {
        return CalcManager.ActionHeal(this, this.Source, potency);
    }


    public bool IsCombo()
    {
        return this.ActionData.ActionCombo.Row != 0;
    }

    public bool IsCorrectCombo()
    {
        var lastActionId = this.Source.LastComboActionId;
        if (lastActionId == 0)
            return false;

        return this.ActionData.ActionCombo.Row == lastActionId;
    }

    public void ExecuteAndSendEffects()
    {
        if (this.AffectedActors.Count == 1)
        {
            this.Source.SendMessageToVisible(new ServerEffect
            {
                Action = this
            }, true);
        }
        else if (this.AffectedActors.Count > 1)
        {
            this.Source.SendMessageToVisible(new ServerAoeEffect8
            {
                Action = this
            }, true);
        }

        foreach (var pair in this.Effects)
        {
            foreach (var effect in pair.Value)
            {
                ActionManager.ExecuteEffect(effect);
            }
                
        }
    }

    public virtual bool Update()
    {
        if (this.StartTime == 0)
            return false;

        if (isInterrupted())
            return true;
            
        ulong time = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();

        if (!HasCastTime() || time - this.StartTime > this.CastTimeMs)
        {
            Execute();
            return true;
        }
        return false;
    }

    public bool FilterActor(Actor actor)
    {
        if (actor.IsCharacter)
        {
            var character = actor.ToChara;
            if (!(this.ActionData.Unknown24 == 0 && character.State == ActorStatus.Dead))
                return true;
        }
        return false;
    }
    public void SnapshotAffectedActors()
    {
        foreach (var actor in this.Source.GetActorsInRange())
        {
            if (!FilterActor(actor))
                continue;

            if ((CastType == CastType.SingleTarget && actor.Id == this.TargetId) || (CastType != CastType.SingleTarget && Vector3.Distance(actor.Position.Offset, this.Position.Offset) <= this.ActionData.EffectRange ))
            {
                this.AffectedActors.Add(actor.ToChara);
            }
        }
    }
}