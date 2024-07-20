using System.IO;
using Shared.Network;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientInventoryAction)]
    public class ClientInventoryAction : SubPacket
    {
        public uint Sequence { get; private set; }
        public InventoryAction Action { get; private set; }
        public ContainerType Source { get; private set; }
        public uint SourceStackSize { get; private set; }
        public ushort SourceSlot { get; private set; }
        public ContainerType Destination { get; private set; }
        public ushort DestinationSlot { get; private set; }
        public uint DestinationStackSize { get; private set; }

        public override void Read(BinaryReader reader)
        {
            Sequence = reader.ReadUInt32();
            Action = (InventoryAction)(reader.ReadUInt16() - SubMessageHeader.Opcode);
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            Source = (ContainerType)reader.ReadUInt32();
            SourceSlot = reader.ReadUInt16();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();

            SourceStackSize = reader.ReadUInt32();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            Destination = (ContainerType)reader.ReadUInt32();
            DestinationSlot = reader.ReadUInt16();
            reader.ReadUInt16();
            DestinationStackSize = reader.ReadUInt32();
        }
    }
}
