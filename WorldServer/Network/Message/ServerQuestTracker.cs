using System.Collections.Generic;
using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerQuestTracker)]
    public class ServerQuestTracker : SubPacket
    {

        public List<QuestModel> Quests;
        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i >= this.Quests.Count)
                {
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                }
                else
                {
                    var quest = this.Quests[i];
                    writer.Write((byte)1);
                    writer.Write((byte)quest.Slot);
                }
                    
            }
            writer.Pad(6u);
        }
    }
}