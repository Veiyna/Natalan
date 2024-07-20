using System.Collections;
using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerQuestJournalCompleteList)]
    public class ServerQuestJournalCompleteList : SubPacket
    {

        public BitArray QuestMask;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.QuestMask.ToArray());
        }
    }
}
