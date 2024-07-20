using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientAdventurerPlateView)]
public class ClientAdventurerPlateView : SubPacket
{
    public ulong Id { get; private set; }
        
    public byte IsActorId { get; private set; }

    public override void Read(BinaryReader reader)
    {
        Id = reader.ReadUInt64();
        reader.Skip(4);
        IsActorId = reader.ReadByte();
    }
}