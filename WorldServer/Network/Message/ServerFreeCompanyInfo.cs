using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerFreeCompanyInfo)]
    
public class ServerFreeCompanyInfo : SubPacket
{
    public FreeCompany FreeCompany;
    public override void Write(BinaryWriter writer)
    {
            if (this.FreeCompany is not null)
            {
                writer.Write(this.FreeCompany.Id);
                writer.Write(this.FreeCompany.ChannelId);
                writer.Write((ulong)1);
                writer.Write((ulong)1);
                writer.Write((uint)1);
                writer.Write((uint)1);
                writer.Write((ushort)1);
                writer.Write((byte)this.FreeCompany.Status);
                writer.Write(this.FreeCompany.GC);
                writer.Write((byte)1);
                writer.Write((byte)1);
                writer.Write((byte)1);
                writer.Write((byte)1);
                writer.Write((byte)1);
            }
            else
            {
                writer.Pad(56u);
            }


        }
}