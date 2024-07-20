using System.IO;
using System.Linq;
using Shared.Database;
using Shared.Database.Datacentre.Models;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Map;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerExamineFreeCompanyInfo)]
    
public class ServerExamineFreeCompanyInfo : SubPacket
{
    public Player Player;

    public FreeCompany FreeCompany;
    public override void Write(BinaryWriter writer)
    {
        if (this.Player == null)
            return;
        if (this.FreeCompany is not null)
        {
            writer.Write(this.Player?.Character.Id ?? this.FreeCompany.Id);
            writer.Write(this.FreeCompany.Id);
            writer.Write(this.FreeCompany.Crest);
            writer.Write((ulong)0);
            writer.Write(this.Player?.Character.ActorId ?? 0);

            writer.Write((uint)1643862024);
            writer.Write((byte)0xc7);
            writer.Write((byte)0xc9);
            writer.Write((byte)0x03);
            switch (this.FreeCompany.GC)
            {
                case 1:
                    writer.Write((byte)this.FreeCompany.GCRep1);
                    break;
                case 2:
                    writer.Write((byte)this.FreeCompany.GCRep2);
                    break;
                case 3:
                    writer.Write((byte)this.FreeCompany.GCRep3);
                    break;
                default:
                    writer.Write((byte)0);
                    break;
                        
            }
                
            writer.Write((ushort)this.FreeCompany.Members.Count); // total online
            writer.Write((ushort)this.FreeCompany.Members.Select(m => MapManager.FindPlayer(m)).Count(p => p != null));
                
            writer.Write((byte)0xee);
            writer.Write((byte)0x05);
            writer.Write((byte)0x1f);
            writer.Write((byte)0x05);
            writer.Write((byte)0x01); // active time
            writer.Write((byte)0x01);
            writer.Write(this.FreeCompany.GC);
            writer.Write((byte)this.FreeCompany.Status);

            writer.Write(this.FreeCompany.Rank);
            writer.Write((byte)0);
            writer.WriteStringLength(this.FreeCompany.Name, 22);
            writer.WriteStringLength(this.FreeCompany.Tag, 7);
            writer.WriteStringLength(DatabaseManager.DataCentre.GetCharacterById(this.FreeCompany.LeaderId).Name,0x20); 
            writer.WriteStringLength(this.FreeCompany.Slogan,193);
            writer.Pad(23);

        }
        else
        {
            writer.Pad(32u);
            writer.Write(this.Player.Character.ActorId);
            writer.Pad(304u);
                
        }
                
            
    }
}