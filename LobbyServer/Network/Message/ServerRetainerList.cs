using System.IO;
using Shared.Network;

namespace LobbyServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerRetainerList)]
    public class ServerRetainerList : SubPacket
    {
        public override void Write(BinaryWriter writer)
        {
        }
    }
}
