using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerMSQTrackerProgress)]
public class ServerMSQTrackerProgress : SubPacket
{
    public uint Id;
    public uint Id2;

    public override void Write(BinaryWriter writer)
    {
            writer.Write(Id);
            writer.Write(Id2);
        }
}