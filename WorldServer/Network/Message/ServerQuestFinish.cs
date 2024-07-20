using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerQuestFinish)]
    public class ServerQuestFinish : SubPacket
    {

        public ushort QuestId;
        public byte Flag1;
        public byte Flag2;
        public override void Write(BinaryWriter writer)
        {   
            writer.Write(QuestId);
            writer.Write(this.Flag1);
            writer.Write(this.Flag2);
            writer.Write((uint)0);
        }
    }
}