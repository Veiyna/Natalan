using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientSocialBlackList)]
    public class ClientSocialBlackList : SubPacket
    {
        
        public byte Sequence { get; private set; }
        public override void Read(BinaryReader reader)
        {
            reader.Skip(1u);
            Sequence = reader.ReadByte();
        }
    }
}