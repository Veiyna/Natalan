using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerTellNotFound)]
public class ServerTellNotFound : SubPacket
{
    public string RecipientName;

    public override void Write(BinaryWriter writer)
    {
        writer.Pad(2);
        writer.WriteStringLength(this.RecipientName, 32);
    }
}