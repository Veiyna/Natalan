using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerMSQTrackerComplete)]
public class ServerMSQTrackerComplete : SubPacket
{
    public uint Id;
    public uint Id2;
    public ulong Unk1;
    public ulong Unk2;
    public ulong Unk3;

    public override void Write(BinaryWriter writer)
    {
            writer.Write(Id);
            writer.Write(Id2);
            writer.Write(this.Unk1);
            writer.Write(this.Unk2);
            writer.Write(this.Unk3);
        }
}