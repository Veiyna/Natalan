using System.Collections;
using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerTitleList)]
public class ServerTitleList : SubPacket
{
    public BitArray TitleList;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(new BitArray(96 * 8, true).ToArray());
    }
}