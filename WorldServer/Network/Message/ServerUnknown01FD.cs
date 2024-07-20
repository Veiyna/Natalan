using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerUnknown0209)]
public class ServerUnknown01FD : SubPacket
{
    public override void Write(BinaryWriter writer)
    {
        writer.Pad(56u);
    }
}