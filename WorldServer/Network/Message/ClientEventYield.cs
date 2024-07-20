using System.IO;
using Shared;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientEventYield2)]
[SubPacket(SubPacketClientHandlerId.ClientEventYield16)]
public class ClientEventYield : SubPacket
{

    public uint EventId { get; private set; }
    public ushort SceneId { get; private set; }
    public byte YieldId { get; private set; }
    public byte ParamCount { get; private set; }
        
    public uint[] Params { get; private set; }
    public override void Read(BinaryReader reader)
    {
        EventId = reader.ReadUInt32();
        SceneId = reader.ReadUInt16();
        YieldId = reader.ReadByte();
        ParamCount = reader.ReadByte();
        Params = reader.ReadUInts(ParamCount);
    }
}