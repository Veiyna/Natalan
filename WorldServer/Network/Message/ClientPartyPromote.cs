using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientPartyPromote)]
public class ClientPartyPromote : SubPacket
{
    public string Name { get; private set; }

    public override void Read(BinaryReader reader)
    {
        reader.Skip(8u);
        reader.Skip(1u);
        reader.Skip(1u);
        Name = reader.ReadStringLength(0x20, true);
        reader.Skip(6u);
    }
}