using System.IO;
using Shared.Network;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientSocialInvite)]
    public class ClientSocialInvite : SubPacket
    {
        public SocialType SocialType { get; private set; }
        public string Invitee { get; private set; }
        
        public byte Player1 { get; private set; }
        public byte Player2 { get; private set; }
        

        public override void Read(BinaryReader reader)
        {
            reader.Skip(8u);
            Player1 = reader.ReadByte();
            Player2 = reader.ReadByte();
            SocialType = (SocialType)reader.ReadByte();
            Invitee    = reader.ReadStringLength(0x20, true);
            reader.Skip(5u);
        }
    }
}
