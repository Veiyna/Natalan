using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerLogout)]
public class ServerLogout : SubPacket
{
    public uint Flag1;
    public uint Flag2;
    public override void Write(BinaryWriter writer)
    {
            writer.Write(this.Flag1);
            writer.Write(this.Flag2);
        }
}