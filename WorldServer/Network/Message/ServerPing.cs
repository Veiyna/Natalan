using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerPing, false)]
public class ServerPing : SubPacket
{
    public ulong Timestamp;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.Timestamp);
        writer.Pad(24);
    }
}