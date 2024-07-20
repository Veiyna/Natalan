using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientZoneLine)]
    public class ClientZoneLine : SubPacket
    {
        public uint ZoneLine { get; private set; }

        public override void Read(BinaryReader reader)
        {
            ZoneLine = reader.ReadUInt32();

        }
    }
}