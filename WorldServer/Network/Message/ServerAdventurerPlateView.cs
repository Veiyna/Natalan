using System.IO;
using Shared.Database.Datacentre;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerAdventurerPlateView)]
public class ServerAdventurerPlateView : SubPacket
{
    public CharacterInfo Character;

    public override void Write(BinaryWriter writer)
    {
        writer.Pad(8u);
        writer.Pad(8u);
        if (Version.Version.StartsWith("7"))
        {
            writer.Write(this.Character.Id);
        }
        writer.Write(Character.ActorId);
        writer.Write(0x2);
        writer.Write(Character.RealmId);
        writer.Write(Character.Class.Level);
        writer.Write(Character.ClassJobId);
        writer.Pad(1u);
        writer.Write(this.Character.GrandCompany);
        writer.Write(this.Character.CurrentGrandCompanyRank);
        if(this.Character.AdventurerPlate.RawData is null)
            writer.Pad(182u);
        else
            writer.Write(this.Character.AdventurerPlate.RawData);
        if (Version.Version.StartsWith("7"))
        {
            writer.Pad(6);
        }
        writer.WriteStringLength(this.Character.SocialInfo.SearchComment, 193);

        writer.WriteStringLength(this.Character.Name, 32);
        if (Version.Version.StartsWith("7"))
        {
            writer.Pad(27);
        }
            
    }
}