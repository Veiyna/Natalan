using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientContentFinderRequestInfo)]
    public class ClientContentFinderRequestInfo : SubPacket
    {
        public override void Read(BinaryReader reader)
        {
            // TODO, 8bytes
        }
    }
}
