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
using WorldServer.Game.Map.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;
using WorldServer.Script;

namespace WorldServer.Game.Map;

public class InstanceContent : Territory
{
    private Lumina.Excel.GeneratedSheets.InstanceContent InstanceContentEntry;
    public InstanceContentScript Script;
    private EventObject EntranceObject;
    private InstanceContentState State;
    
    public InstanceContent(TerritoryType entry) : base(entry)
    {
        var contentFinderCondition = entry.ContentFinderCondition.Value;
        this.InstanceContentEntry = GameTableManager.InstanceContent.GetRow(contentFinderCondition.Content);
        this.Director = new Director(this,DirectorType.InstanceContent, (ushort)this.InstanceContentEntry.RowId, (ushort)contentFinderCondition.RowId ); 
        this.Script    = ScriptManager.NewInstanceContentScript(this.InstanceContentEntry.RowId);
        this.Script?.Initialise(this);
        

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

    public void DutyComplete()
    {
        SendToAll(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorUpdate,
            Parameter1 = this.Director.DirectorId,
            Parameter2 = (uint)DirectorEventId.DutyComplete,
            Parameter3 = this.InstanceContentEntry.WinBGM.Row
        });
    }
    

    public void BeginDuty()
    {
        SendToAll(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorUpdate,
            Parameter1 = this.Director.DirectorId,
            Parameter2 = (uint)DirectorEventId.DutyCommence,
            Parameter3 = this.InstanceContentEntry.TimeLimitmin * 60u
        });
        this.EntranceObject?.UpdatePermissionInvisibility(1);
        this.State = InstanceContentState.DutyInProgress;


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
            var rect = GameTableManager.GetLGBEntry(LayerEntryType.EventRange, this.InstanceContentEntry.LGBEventRange);
            
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