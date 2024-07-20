using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerPerformNote)]
public class ServerPerformNote : SubPacket
{
    public byte[] Data;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.Data);
    }
}