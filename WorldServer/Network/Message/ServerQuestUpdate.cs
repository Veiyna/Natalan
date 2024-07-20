using System.IO;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerQuestUpdate)]
    public class ServerQuestUpdate : SubPacket
    {

        public QuestModel Quest;
        public override void Write(BinaryWriter writer)
        {
            writer.Write((uint)this.Quest.Slot);  
            writer.Write((ushort)this.Quest.QuestId);
            writer.Write(this.Quest.Sequence);
            writer.Write((ushort)this.Quest.Flags);
            if(Version.Version.StartsWith('7'))
                writer.Pad(5);
            writer.Write(this.Quest.Data1);
            
            writer.Write(this.Quest.Data2);
            
            writer.Write(this.Quest.Data3);
            
            writer.Write(this.Quest.Data4);
            
            writer.Write(this.Quest.Data5);
            
            writer.Write(this.Quest.Data6);
            writer.Pad(1u);
            
        }
    }
}