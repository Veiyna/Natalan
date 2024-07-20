using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientEquipDisplayFlags)]
public class ClientEquipDisplayFlags : SubPacket
{
    public byte DisplayFlags;

    public override void Read(BinaryReader reader)
    {
        this.DisplayFlags = reader.ReadByte();
    }
}