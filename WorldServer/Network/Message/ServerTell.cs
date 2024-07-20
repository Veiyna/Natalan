using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerTell)]
public class ServerTell : SubPacket
{
    public ulong ContentId;
    public ushort WorldId;
    public byte Flags;
    public string ReceipientName;
    public string Message;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.ContentId);
        if (Version.Version.StartsWith('7')) // DAWNTRAIL BLACKLIST UPDATE
        {
            writer.Write((ulong)10); // account id here
        }
        writer.Write(this.WorldId);
        writer.Write(this.Flags);
        writer.WriteStringLength(this.ReceipientName, 32);
        writer.WriteStringLength(this.Message, 1029);
    }
}