using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerFreeCompanyActivity)]
    
public class ServerFreeCompanyActivity : SubPacket
{
    public FreeCompany FreeCompany;
    public override void Write(BinaryWriter writer)
    { 
        writer.Pad(528u);
    }
}