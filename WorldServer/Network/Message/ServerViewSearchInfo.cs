using System.IO;
using Shared.Database.Datacentre;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerViewSearchInfo)]
    public class ServerViewSearchInfo : SubPacket
    {
        public CharacterInfo Character;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Character.Id);
            writer.Pad(16u);
            writer.Pad(8u);
            writer.Pad(2u);
            writer.Write(this.Character.RealmId);
            writer.WriteStringLength(this.Character.SocialInfo.SearchComment, 193);
            writer.Pad(23u);
            writer.Write(this.Character.GrandCompanyRanks);
            writer.Pad(1u);
            for (uint i = 1; i <= 40; i++)
            {
                var classInfo = this.Character.GetClassInfo(i);
                writer.Write((ushort)i);
                writer.Write(classInfo.Level);
            }
        }
    }
}