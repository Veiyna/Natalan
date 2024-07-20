using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientSkill)]
    
public class ClientSkill : SubPacket
{

    public byte Type { get; private set; }
    public uint ActionId { get; private set; }
    public ushort Sequence { get; private set; }
    public ulong TargetId { get; private set; }
    public ushort ItemSlot { get; private set; }
    public ushort ItemContainer { get; private set; }
    public override void Read(BinaryReader reader)
    {
        reader.Skip(1u);
        Type = reader.ReadByte();
        reader.Skip(2u);
        ActionId = reader.ReadUInt32();
        Sequence = reader.ReadUInt16();
        reader.Skip(6u);
        TargetId = reader.ReadUInt64();
        ItemSlot = reader.ReadUInt16();
        ItemContainer = reader.ReadUInt16();
    }
}