using System.IO;
using Shared.Network;
using WorldServer.Game.Event;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerEventYield32)]
public class ServerEventYield32 : SubPacket
{
    public Event Event;
    public byte[] Data;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Event.Id);
        writer.Write(Event.ActiveScene.Id);
        writer.Write(Data);
    }
}