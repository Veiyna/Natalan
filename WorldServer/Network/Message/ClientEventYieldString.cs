using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientEventYieldString8)]
    [SubPacket(SubPacketClientHandlerId.ClientEventYieldString16)]
    [SubPacket(SubPacketClientHandlerId.ClientEventYieldString32)]
    public class ClientEventYieldString : SubPacket
    {

        public uint EventId { get; private set; }
        public ushort SceneId { get; private set; }
        public byte YieldId { get; private set; }
        
        public string Value { get; private set; }
        public override void Read(BinaryReader reader)
        {
            EventId = reader.ReadUInt32();
            SceneId = reader.ReadUInt16();
            YieldId = reader.ReadByte();
            Value = reader.ReadStringNull();
        }
    }
}