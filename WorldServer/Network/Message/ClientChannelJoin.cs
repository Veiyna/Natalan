using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientChannelJoin)]
    public class ClientChannelJoin : SubPacket
    {
        public ulong ChannelId;

        public override void Read(BinaryReader reader)
        {
            this.ChannelId = reader.ReadUInt64();
        }
    }
}