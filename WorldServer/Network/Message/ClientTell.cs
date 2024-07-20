using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientTell)]
    public class ClientTell : SubPacket
    {
        public ulong CharacterId { get; private set; }
        public ushort WorldId { get; private set; }
        public ushort WorldId2 { get; private set; }
        public string TargetName { get; private set; }
        public string Message { get; private set; }

        public override void Read(BinaryReader reader)
        {
            CharacterId = reader.ReadUInt64();
            WorldId = reader.ReadUInt16();
            reader.ReadUInt16();
            reader.ReadUInt32();
            WorldId2 = reader.ReadUInt16();
            reader.ReadByte();
            TargetName = reader.ReadStringLength(32, true);
            Message = reader.ReadStringLength(1029, true);
            
        }
    }
}