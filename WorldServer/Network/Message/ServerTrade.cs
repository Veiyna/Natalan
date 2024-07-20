using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerTrade)]
    public class ServerTrade : SubPacket
    {
        public uint Type;
        public uint ActorId;
        public override void Write(BinaryWriter writer)
        {
            writer.Write((uint)0);
            writer.Write(Type);
            writer.Write((ulong)0);
            writer.Write((ulong)0);
            writer.Write((ulong)0);
            writer.Write((ulong)0);
            writer.Write(ActorId);
            writer.Write((uint)0);
        }
    }
}