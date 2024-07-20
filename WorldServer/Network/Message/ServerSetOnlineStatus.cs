using System.Collections;
using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerSetOnlineStatus)]
    public class ServerSetOnlineStatus : SubPacket
    {

        public BitArray onlineStatus;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.onlineStatus.ToArray());
        }
    }
}