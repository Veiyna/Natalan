using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientTrade)]
    public class ClientTrade : SubPacket
    {
        public byte Sequence { get; private set; }
        public byte Type { get; private set; }
        public uint ActorId { get; private set; }
        

        public override void Read(BinaryReader reader)
        {
            Sequence = reader.ReadByte();
            reader.Skip(3);
            Type = reader.ReadByte();
            reader.Skip(3);
            ActorId = reader.ReadUInt32();

        }
    }
}