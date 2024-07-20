using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerEquipDisplayFlags)]
    public class ServerEquipDisplayFlags : SubPacket
    {
        public byte DisplayFlags;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.DisplayFlags);
        }
    }
}