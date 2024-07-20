using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerFreeCompanyBoard)]
    
public class ServerFreeCompanyBoard : SubPacket
{
    public FreeCompany FreeCompany;
    public override void Write(BinaryWriter writer)
    {
            if (this.FreeCompany is not null)
            {
                writer.Write((byte)2);
                writer.WriteStringLength(this.FreeCompany.Board,193);

            }
            else
            {
                writer.Pad(200u);
            }


        }
}