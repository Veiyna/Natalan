using System.IO;
using Shared;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientEventSceneFinish)]
[SubPacket(SubPacketClientHandlerId.ClientEventSceneFinish2)]
[SubPacket(SubPacketClientHandlerId.ClientEventSceneFinish3)]
public class ClientEventSceneFinish : SubPacket
{
    public uint EventId { get; private set; }
    public ushort SceneId { get; private set; }
        
    public byte ErrorCode { get; private set; }
    public byte ParamCount { get; private set; }
    public uint[] Data { get; private set; }

    public override void Read(BinaryReader reader)
    {
        EventId = reader.ReadUInt32();
        SceneId = reader.ReadUInt16();
        ErrorCode = reader.ReadByte();
        ParamCount = reader.ReadByte();
        Data = reader.ReadUInts(ParamCount);

    }
}