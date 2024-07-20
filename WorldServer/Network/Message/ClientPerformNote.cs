using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientPerformNote)]
public class ClientPerformNote : SubPacket
{
    public byte[] Data;

    public override void Read(BinaryReader reader)
    {
        this.Data = reader.ReadBytes(16);
    }
}