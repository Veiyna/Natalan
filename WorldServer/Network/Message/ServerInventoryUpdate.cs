using System.IO;
using Shared.Network;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerInventoryUpdate)]
    public class ServerInventoryUpdate : SubPacket
    {
        public uint Id;
        public InventoryAction Action;

        public uint SrcActorId;
        public ContainerType SrcContainerType;
        public ushort SrcSlot;
        public uint SrcStackSize;
        public uint SrcItemId;

        public uint DstActorId;
        public ContainerType DstContainerType = ContainerType.None;
        public ushort DstSlot = ushort.MaxValue;
        public uint DstStackSize;
        public uint DstItemId;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write((uint)Action + Version.ClientOpcodes[SubPacketClientHandlerId.ClientInventoryAction]);

            writer.Write(SrcActorId);
            writer.Write((uint)SrcContainerType);
            writer.Write(SrcSlot);
            writer.Pad(2u);
            writer.Write(SrcStackSize);
            writer.Write(SrcItemId);

            writer.Write(DstActorId);
            writer.Write((uint)DstContainerType);
            writer.Write(DstSlot);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(DstStackSize);
            writer.Write(DstItemId);
        }
    }
}
