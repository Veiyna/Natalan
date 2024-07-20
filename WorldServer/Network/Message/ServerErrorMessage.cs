using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerErrorMessage)]
    public class ServerErrorMessage : SubPacket
    {
        public uint Id;


        public override void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write((uint)0x00270db6);
            writer.Pad(16u);

            
            
            
        }
    }
}