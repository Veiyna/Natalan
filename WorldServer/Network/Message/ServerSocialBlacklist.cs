using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerSocialBlacklist)]
    public class ServerSocialBlacklist : SubPacket
    {
        
        public byte Sequence;
        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 20; i++)
            {
                writer.Write(0ul); // characterId
                writer.WriteStringLength("", 0x20);
            }
            
            if (Version.Version.StartsWith('7')) // DAWNTRAIL
            {
                writer.Pad(160);
            }

            writer.Pad(2u);
            writer.Write((ushort)this.Sequence);
            writer.Pad(4u);

                
            
        }
    }
}
