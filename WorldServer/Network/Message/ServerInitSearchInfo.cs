using System.Collections;
using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerInitSearchInfo)]
    public class ServerInitSearchInfo : SubPacket
    {
        public BitArray OnlineStatus;


        public CharacterSocialInfo SocialInfo;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.OnlineStatus.ToArray());
            writer.Pad(8u);
            writer.Pad(1u);
            writer.Write(this.SocialInfo.SelectRegion);
            writer.WriteStringLength(this.SocialInfo.SearchComment, 193);
            writer.Pad(5u);
        }
    }
}