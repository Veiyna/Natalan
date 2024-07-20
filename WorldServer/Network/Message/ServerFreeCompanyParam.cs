using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerFreeCompanyParam)]
    
public class ServerFreeCompanyParam : SubPacket
{
    public FreeCompany FreeCompany;
    public override void Write(BinaryWriter writer)
    {
            if (this.FreeCompany is not null)
            {
                writer.Write((ulong)0);
                writer.Write(this.FreeCompany.Points);
                writer.Write(this.FreeCompany.Credit);
                writer.Write(this.FreeCompany.Credit);
                writer.Write((uint)0);
                writer.Write((uint)999999999);
                writer.Write((uint)this.FreeCompany.Points);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)this.FreeCompany.GCRep1);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)this.FreeCompany.GCRep2);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)this.FreeCompany.GCRep3);
                writer.Write(this.FreeCompany.Rank);
                writer.Pad(7u);
                

            }
            else
            {
                writer.Pad(64u);
            }


        }
}