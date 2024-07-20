using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerSystemLogMessage)]
    public class ServerSystemLogMessage : SubPacket
    {
        public uint Id;
        public byte Parameter1;
        public byte Parameter2;
        public byte Parameter3;
        public byte Parameter4;
        public byte Parameter5;
        public byte Parameter6;
        public byte Parameter7;
        public byte Parameter8;
        public byte Parameter9;
        public byte Parameter10;
        public byte Parameter11;
        public byte Parameter12;
        public byte Parameter13;
        public byte Parameter14;
        public byte Parameter15;
        public byte Parameter16;

        public override void Write(BinaryWriter writer)
        {
            writer.Pad(4u);
            writer.Write(Id);
            writer.Write(Parameter1);
            writer.Write(Parameter2);
            writer.Write(Parameter3);
            writer.Write(Parameter4);
            writer.Write(Parameter5);
            writer.Write(Parameter6);
            writer.Write(Parameter7);
            writer.Write(Parameter8);
            writer.Write(Parameter9);
            writer.Write(Parameter10);
            writer.Write(Parameter11);
            writer.Write(Parameter12);
            writer.Write(Parameter13);
            writer.Write(Parameter14);
            writer.Write(Parameter15);
            writer.Write(Parameter16);
            
            
            
        }
    }
}