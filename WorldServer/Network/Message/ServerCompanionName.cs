using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerCompanionName)]
public class ServerCompanionName : SubPacket
{
    public string Name;
        
    public override void Write(BinaryWriter writer)
    {
        writer.Pad(1);
        writer.WriteStringLength(this.Name, 39);
    }
}