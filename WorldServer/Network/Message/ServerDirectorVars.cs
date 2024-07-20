using Shared.Network;
using System.IO;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerDirectorVars)]
    public class ServerDirectorVars : SubPacket
    {
        public uint DirectorId;
        public byte Sequence;
        public byte Branch;
        public byte[] Data;
        
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.DirectorId);
            writer.Write(this.Sequence);
            writer.Write(this.Branch);
            writer.Write(this.Data);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
        }
    }
}