using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerFreeCompanySlogan)]
    
    public class ServerFreeCompanySlogan : SubPacket
    {
        public FreeCompany FreeCompany;
        public override void Write(BinaryWriter writer)
        {
            if (this.FreeCompany is not null)
            {
                writer.Write((ushort)4);
                writer.Write((ushort)0);
                writer.Write((byte)2);
                writer.Write((byte)2);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.WriteStringLength(this.FreeCompany.Slogan,193);

            }
            else
            {
                writer.Pad(201u);
            }
        }
    }
}