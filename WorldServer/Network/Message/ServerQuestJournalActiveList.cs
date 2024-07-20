using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerQuestJournalActiveList)]
    public class ServerQuestJournalActiveList : SubPacket
    {

        public List<QuestModel> Quests;
        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 30; i++)
            {

                var quest = this.Quests.ElementAtOrDefault(i);
                writer.Write(quest?.QuestId ?? 0);
                writer.Write(quest?.Sequence ?? 0);
                writer.Write((ushort)(quest?.Flags ?? 0));
                if (Version.Version.StartsWith('7'))
                    writer.Pad(5);
                writer.Write(quest?.Data1 ?? 0);
                writer.Write(quest?.Data2 ?? 0);
                writer.Write(quest?.Data3 ?? 0);
                writer.Write(quest?.Data4 ?? 0);
                writer.Write(quest?.Data5 ?? 0);
                writer.Write(quest?.Data6 ?? 0);
                if (!Version.Version.StartsWith('7'))
                    writer.Pad(1);
            }

        }
    }
}