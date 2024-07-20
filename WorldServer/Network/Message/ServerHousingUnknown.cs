using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerHousingUnknown)]
    public class ServerHousingUnknown : SubPacket
    {
        public override void Write(BinaryWriter writer)
        {
            writer.Pad(48);
        }
        
    }
}