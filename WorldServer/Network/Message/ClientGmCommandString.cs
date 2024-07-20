using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientGmCommandString)]
public class ClientGmCommandString : SubPacket
{
    public GmCommand Command { get; private set; }
    public GmCommandParameters Parameters { get; private set; }

    public override void Read(BinaryReader reader)
    {
        Command    = (GmCommand)reader.ReadUInt32();
        var param = reader.ReadBytes(16);
        reader.Skip(2u);
        Parameters = new GmCommandParameters(param, 0, reader.ReadStringLength(0x20u, true));
        reader.Skip(20u);
    }
}