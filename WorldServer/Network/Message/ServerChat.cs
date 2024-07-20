using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerChat)]
    public class ServerChat : SubPacket
    {
        public ulong AccountId;
        public ulong Id;
        public ushort Type;
        public string Name;
        public string Message;
        
        public override void Write(BinaryWriter writer)
        {
            if(Version.Version.StartsWith('7'))
            {
                writer.Write(this.AccountId);
                writer.Write(this.Id);
                writer.Pad(6u); 
            }
            else
            {
                writer.Pad(6u); 
                writer.Pad(8u);
            }
            
            writer.Write(Type);
            writer.WriteStringLength(Name, 0x20);
            writer.WriteStringLength(Message, 1028);
        }
    }
}
