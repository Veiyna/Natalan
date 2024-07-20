using System.IO;
using Shared.Network;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerInventoryAction)]
    public class ServerInventoryAction : SubPacket
    {
        public uint Sequence;
        public InventoryAction Type;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Sequence);
            writer.Write((uint)Type);
            writer.Pad(8u);
        }
    }
}