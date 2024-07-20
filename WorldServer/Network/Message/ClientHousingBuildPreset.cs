using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientHousingBuildPreset)]
    public class ClientHousingBuildPreset : SubPacket
    {
        public uint ItemId { get; private set; }
        public byte PlotNumber { get; private set; }
        public string EstateName { get; private set; }

        public override void Read(BinaryReader reader)
        {
            ItemId = reader.ReadUInt32();
            PlotNumber = reader.ReadByte();
            EstateName = reader.ReadStringLength(27);
        }
    }
}