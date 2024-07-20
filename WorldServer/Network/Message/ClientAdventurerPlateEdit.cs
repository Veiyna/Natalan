using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientAdventurerPlateEdit)]
    public class ClientAdventurerPlateEdit : SubPacket
    {
        public byte Type;

        public byte[] PlateData;

        public override void Read(BinaryReader reader)
        {
            Type = reader.ReadByte();
            reader.Skip(3u);
            PlateData = reader.ReadBytes(182); // endwalker 172, dawntrail 182
        }
    }
}