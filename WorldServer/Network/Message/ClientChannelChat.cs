using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientChannelChat)]
public class ClientChannelChat : SubPacket
{
    public ulong ChannelId;
    public string Message;

    public override void Read(BinaryReader reader)
    {
        this.ChannelId = reader.ReadUInt64();
        this.Message = reader.ReadStringLength(1024);
        reader.ReadUInt64();
        reader.ReadUInt64();
    }
}