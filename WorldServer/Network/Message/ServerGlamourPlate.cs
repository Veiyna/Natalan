using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerGlamourPlate)]
    
    public class ServerGlamourPlate : SubPacket
    {

        public override void Write(BinaryWriter writer)
        {
        }
    }
}