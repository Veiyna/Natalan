using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientExamineFreeCompanyInfo)]
    public class ClientExamineFreeCompanyInfo : SubPacket
    {
        public ulong FreeCompanyOrPlayerId;
        public uint ActorId { get; private set; }
        
        public uint Flag { get; private set; }
        
        public override void Read(BinaryReader reader)
        {
            this.FreeCompanyOrPlayerId = reader.ReadUInt64();
            ActorId = reader.ReadUInt32();
            this.Flag = reader.ReadUInt32();
            
        }
    }
}