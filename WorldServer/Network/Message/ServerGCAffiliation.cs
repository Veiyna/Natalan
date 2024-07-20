using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerGCAffiliation)]
    
    public class ServerGCAffiliation : SubPacket
    {
        public byte GCId;
        public byte[] GCRanks = new byte[3];
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.GCId);
            writer.Write(this.GCRanks);
        }
    }
}