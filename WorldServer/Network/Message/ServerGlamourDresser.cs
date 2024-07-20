using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Database.Datacentre.Models;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerGlamourDresser)]
    
    public class ServerGlamourDresser : SubPacket
    {
        public uint Sequence;
        public List<GlamourDresserEntry> Entries;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(Sequence);

            for (int i = 0; i < 400; i++)
            {
                    var entry = this.Entries.ElementAtOrDefault(i);
                    var itemId = entry?.ItemId ?? 0;
                    writer.Write(itemId);
            }
            
            for (int i = 0; i < 400; i++)
            {
                    var entry = this.Entries.ElementAtOrDefault(i);
                    var color = (byte)(entry?.Color ?? 0);
                    writer.Write(color);
            }
        }
    }
}