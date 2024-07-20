using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientNoviceNetworkRequestJoin)]
    public class ClientNoviceNetworkRequestJoin : SubPacket
    {
        public ulong Unknown { get; private set; }

        public override void Read(BinaryReader reader)
        {
            Unknown = reader.ReadUInt64();
        }
    }
}