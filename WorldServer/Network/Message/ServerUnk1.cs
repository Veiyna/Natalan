using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerUnk1)]
    public class ServerUnk1 : SubPacket
    {
        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)1);
            writer.Pad(7u);
        }
    }
}