using System.IO;
using Shared;
using Shared.Network;
using WorldServer.Game.Event;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerEventSceneStart255)]
public class ServerEventSceneStart255 : SubPacket
{
    public Event Event;
    public SceneFlags Flags;
    public uint ParamCount;
    public uint[] Params = new uint[255];
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Event.ActorId);
        writer.Write(Event.Id);
        writer.Write(Event.ActiveScene.Id);
        writer.Pad(2u);
        writer.Write((uint)Flags);
        writer.Write(ParamCount);
        writer.Write(this.Params.ToByteArray());
    }
}