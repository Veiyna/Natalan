using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerFreeCompanyDialog)]
    
    public class ServerFreeCompanyDialog : SubPacket
    {
        public FreeCompany FreeCompany;
        public override void Write(BinaryWriter writer)
        {
            if (this.FreeCompany is not null)
            {
                writer.Write(this.FreeCompany.Id);
                writer.Write(this.FreeCompany.Crest);
                writer.Write(this.FreeCompany.Points);
                writer.Write(this.FreeCompany.Credit);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
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
                writer.Write((uint)999999999);
                writer.Write((uint)this.FreeCompany.Points);
                writer.Write((ushort)this.FreeCompany.Members.Count);
                writer.Write((ushort)this.FreeCompany.Members.Count);
                writer.Write(this.FreeCompany.GC);
                writer.Write(this.FreeCompany.Rank);
                writer.WriteStringLength(this.FreeCompany.Name, 22);
                writer.WriteStringLength(this.FreeCompany.Tag, 8);

            }
            else
            {
                writer.Pad(80u);
            }


        }
    }
}