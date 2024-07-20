using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientPing, false)]
    public class ClientPing : SubPacket
    {
        public uint Timestamp;
        public override void Read(BinaryReader reader)
        {
            this.Timestamp = reader.ReadUInt32();
        }
    }
}