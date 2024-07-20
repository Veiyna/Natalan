using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientViewSearchInfo)]
    public class ClientViewSearchInfo : SubPacket
    {
        public ulong CharacterId { get; private set; }

        public override void Read(BinaryReader reader)
        {
            CharacterId = reader.ReadUInt64();

        }
    }
}