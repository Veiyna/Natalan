using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerNoviceNetworkJoined)]
    public class ServerNoviceNetworkJoined : SubPacket
    {

        public ulong ChannelId;
        public ulong PlayerId;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.ChannelId);
            writer.Write((ushort)1);
            writer.Write((ushort)1);
            writer.Pad(4);
            writer.Write(this.PlayerId);
            writer.Write((ulong)0);
        }
        
    }
}