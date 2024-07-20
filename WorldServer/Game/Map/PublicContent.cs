using System.Numerics;
using Lumina.Data.Parsing.Layer;
using Lumina.Excel.GeneratedSheets;
using Shared;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;
using WorldServer.Game.Event.Director;
using WorldServer.Game.Event.Director.Enum;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Map;

public class PublicContent : Territory
{
    private Lumina.Excel.GeneratedSheets.PublicContent PublicContentEntry;
    public InstanceContentScript Script;
    private EventObject EntranceObject;
    
    public PublicContent(TerritoryType entry) : base(entry)
    {
        var contentFinderCondition = entry.ContentFinderCondition.Value;
        this.PublicContentEntry = GameTableManager.PublicContent.GetRow(contentFinderCondition.Content);
        this.Director = new Director(this,DirectorType.PublicContent, (ushort)this.PublicContentEntry.RowId, (ushort)contentFinderCondition.RowId ); 
    }

    public override void OnInstanceRegister()
    {
        this.Script?.OnInit();
    }

    public void SetVar(byte index, byte value) => this.Director.SetVar(index, value);
    public byte GetVar(byte index)
    {
        return Director.GetVar(index);
    }

    
    public override void OnRegisterEObj(EventObject eObj)
    {
        if (eObj.ObjectId == 2000182)
            this.EntranceObject = eObj;

    }

    protected override void AfterAdd(Actor actor)
    {
        base.AfterAdd(actor);
        if (actor.IsPlayer)
        {
            var player = actor.ToPlayer;
            player.SetStateFlag(PlayerStateFlag.BoundByDuty);
            this.Director.SendDirectorInit(player);
            var rect = GameTableManager.GetLGBEntry(LayerEntryType.EventRange, this.PublicContentEntry.LGBEventRange);
            
            if(this.EntranceObject != null)
            {
                player.ChangePos(this.EntranceObject.Position);
            }
            else if (rect.Item2 != null)
            {
                var instanceObject = rect.Item2.Value;
                player.Position.Relocate(
                    new Vector3(instanceObject.Transform.Translation.X, instanceObject.Transform.Translation.Y, instanceObject.Transform.Translation.Z),
                    Utilities.EulerToDirection(new Vector3(instanceObject.Transform.Rotation.X, instanceObject.Transform.Rotation.Y,
                        instanceObject.Transform.Rotation.Z)));
                player.ChangePos(player.Position);
            }
            
        }
    }

    public override void OnZoneIn(Player player)
    {
        BeginDuty();
    }

    public void BeginDuty()
    {
        SendToAll(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorUpdate,
            Parameter1 = this.Director.DirectorId,
            Parameter2 = (uint)DirectorEventId.DutyCommence,
            Parameter3 = (uint)(this.PublicContentEntry.TimeLimit * 60)
        });
        this.EntranceObject?.UpdatePermissionInvisibility(1);
    }

    protected override void AfterRemove(Actor actor)
    {
        base.AfterRemove(actor);
        
        
        if (actor.IsPlayer)
        {
            var player = actor.ToPlayer;
            player.UnsetStateFlag(PlayerStateFlag.BoundByDuty);
            this.Script?.OnLeaveTerritory(player);
            this.Director.SendDirectorClear(player);
        }
    }
}