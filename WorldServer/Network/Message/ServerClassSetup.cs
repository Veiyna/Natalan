using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerClassSetup)]
    public class ServerClassSetup : SubPacket
    {
        public byte ClassJobId;
        public ushort Level;
        
        public override void Write(BinaryWriter writer)
        {
            writer.Write(ClassJobId);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(Level); // XP bar level
            writer.Write(Level); // Class/Job tab level
            writer.Write((uint)140);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
        }
    }
}
