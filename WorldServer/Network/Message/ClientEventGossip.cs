using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientEventGossip)]
    public class ClientEventGossip : SubPacket
    {
        public ulong ActorId { get; private set; }
        public uint EventId { get; private set; }

        public override void Read(BinaryReader reader)
        {
            ActorId = reader.ReadUInt64();
            EventId = reader.ReadUInt32();
            reader.Skip(4u);
        }
    }
}
