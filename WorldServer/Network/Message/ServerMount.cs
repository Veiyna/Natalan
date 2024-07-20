using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerMount)]
public class ServerMount : SubPacket
{
    public uint Id;
    public ushort Color;

    public override void Write(BinaryWriter writer)
    {
        writer.Write((ushort)Id);
        writer.Write(Color);
        writer.Pad(12u);
    }
}