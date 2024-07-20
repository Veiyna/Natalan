using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientCFCommence)]
    public class ClientCFCommence : SubPacket
    {
        public byte Param;

        public override void Read(BinaryReader reader)
        {
            Param = reader.ReadByte();
        }
    }
}