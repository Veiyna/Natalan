using System.IO;
using Shared.Network;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerItemUpdate)]
    public class ServerItemUpdate : SubPacket
    {
        public uint Index;
        public ContainerType ContainerType;
        public ushort Slot;
        public uint StackSize;
        public uint ItemId;
        public ushort Color;
        public ushort Color2;
        public uint Glam = 0;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(0u);
            writer.Write((ushort)ContainerType);
            writer.Write(Slot);
            writer.Write(StackSize);
            writer.Write(ItemId);
            writer.Pad(14u);
            writer.Write((ushort)30000);
            writer.Pad(2u);
            writer.Write(Color);
            writer.Write(Glam);
            writer.Pad(15u);
            writer.Write((byte)this.Color);
            writer.Write((byte)this.Color2);
            writer.Pad(3);
        }
    }
}
