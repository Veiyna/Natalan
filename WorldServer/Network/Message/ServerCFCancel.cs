using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerCFCancel)]
public class ServerCFCancel : SubPacket
{

    public override void Write(BinaryWriter writer)
    {
        writer.Write((uint)0);
        writer.Write((uint)0);
        writer.Write((ulong)890); //logmsg

    }
}