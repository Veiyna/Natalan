using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerCharaVisualEffect)]
public class ServerCharaVisualEffect : SubPacket
{
    public ushort Effect;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Effect);

    }
}