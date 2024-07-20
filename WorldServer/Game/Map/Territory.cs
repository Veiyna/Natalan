using System.Linq;
using System.Numerics;
using Lumina.Excel.GeneratedSheets;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Entity;
using WorldServer.Game.Event.Director;
using WorldServer.Game.Navigation;
using BaseParam = WorldServer.Game.Entity.Enums.BaseParam;

namespace WorldServer.Game.Map;

public class Territory : BaseMap
{
    public TerritoryType Entry { get; }
    public byte Weather { get; private set; }
    public uint InstanceId;
    public Director Director;
        

    public NavigationProvider NavigationProvider;
        
    public Territory(TerritoryType entry)
    {
        Entry = entry;
        var name = entry.GetBgName();
        var navMesh = DataManager.GetNavMesh(name);
        if (navMesh != null)
        {
            this.NavigationProvider = new NavigationProvider(navMesh);
        }


    }

    public virtual void OnInstanceRegister()
    {
        foreach (var bNpcTemplate in DataManager.BNpcTemplates.Values)
        {
            var territoryName = bNpcTemplate.TerritoryName.Trim();
            if (Entry.Name.ToString().StartsWith(territoryName))
            {
                if (bNpcTemplate.Nonpop == 0 && bNpcTemplate.NameId == 541)
                {
                    CreateBNpcFromLayoutId(bNpcTemplate.instanceId);
                }
            }
        }
    }
        
        
    public virtual void OnZoneIn(Player player)
    {

    }

    public EventObject GetEObj(uint actorId)
    {
        return EventObjects.FirstOrDefault(e => e.Id == actorId);
    }
        
    public EventObject GetEObjByName(string name)
    {
        return EventObjects.FirstOrDefault(e => e.Name == name);
    }
        
    public EventObject GetFirstEObjByObjectId(uint objectId)
    {
        return EventObjects.FirstOrDefault(e => e.ObjectId == objectId);
    }

    public BNpc CreateBNpcFromLayoutId(uint layoutId, uint triggerId = 0)
    {
        var bnpctemplate = DataManager.GetBNpcTemplate(layoutId);
        var level = GameTableManager.Level.GetRow(layoutId);
        var bnpc = new BNpc(new WorldPosition((ushort)this.Entry.RowId, Vector3.Zero, 0, this.InstanceId));
        if (bnpctemplate is not null)
        {
            bnpc.InitFromTemplate(bnpctemplate);
        }
        else if (level is not null)
        {
            bnpc.InitFromLevel(level.RowId);
        }

        bnpc.TriggerOwnerId = triggerId;
        bnpc.SetBaseStat(BaseParam.HP, 500);
        bnpc.HP = bnpc.MaxHP;
        bnpc.AddToMap();
        return bnpc;
    }

    public EventObject RegisterEObj(string name, uint objectId, uint mapLink, uint instanceId, byte state, Vector3 position, float scale, float rotation, byte permissionInvisibility = 0)
    {
        var eObj = new EventObject(new WorldPosition((ushort)this.Entry.RowId, position, rotation, this.InstanceId))
        {
            Scale = scale,
            State = state,
            ObjectId = objectId,
            GimmickId = mapLink,
            Name = name,
            PermissionInvisibility = permissionInvisibility
        };
        eObj.AddToMap();
        OnRegisterEObj(eObj);
        return eObj;
            
    }
        

    public virtual void OnRegisterEObj(EventObject eObj)
    {
            
    }

    public override void Update(double lastTick, long tickCount)
    {
        var dt = (tickCount - this.LastUpdate)/1000f;
        this.NavigationProvider?.Update(dt);
        base.Update(lastTick, tickCount); 
            
    }

    protected override void AfterAdd(Actor actor)
    {
        base.AfterAdd(actor);
        if(actor.IsCharacter)
            this.NavigationProvider?.AddAgent(actor.ToChara);
    }

    protected override void AfterRemove(Actor actor)
    {
        base.AfterRemove(actor);
        if(actor.IsCharacter)
            this.NavigationProvider?.RemoveAgent(actor.ToChara);
    }
}