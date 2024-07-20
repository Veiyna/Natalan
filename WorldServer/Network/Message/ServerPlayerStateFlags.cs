using System.Collections;
using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerPlayerStateFlags)]
    public class ServerPlayerStateFlags : SubPacket
    {
        public BitArray StateMask { get; set; }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(StateMask.ToArray());
            writer.Pad(6u);
        }
    }
}
