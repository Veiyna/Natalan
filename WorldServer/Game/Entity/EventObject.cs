using System.Text;
using Shared.Cryptography;
using Shared.Game;
using WorldServer.Game.Entity.Enums;
using WorldServer.Manager;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public class EventObject : Actor
{
    public byte State;
    public uint HousingLink;
    public uint ObjectId;
    public uint GimmickId;
    public ushort Flag;
    public float Scale = 1;
    public byte PermissionInvisibility;
    public string Name;
    public EventObject(WorldPosition position)
        : base(XxHash.CalculateHash(Encoding.UTF8.GetBytes($"{AssetManager.NextBNpcId}::EOBJ")), ActorType.EObj)
    {
        Position = position;
    }

    public void UpdateState(byte state)
    {
        this.State = state;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.EObjSetState,
            Parameter1 = state,
            Parameter2 = 2416312328
        });


    }

    public void UpdatePermissionInvisibility(byte value)
    {
        this.PermissionInvisibility = value;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.DirectorEObjMod,
            Parameter1 = value
        });
    }
}