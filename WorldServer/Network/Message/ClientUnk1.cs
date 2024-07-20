using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientUnk1)]
    public class ClientUnk1 : SubPacket
    {
        public ulong Unk { get; private set; }

        public override void Read(BinaryReader reader)
        {
            Unk = reader.ReadUInt64();

        }
    }
}