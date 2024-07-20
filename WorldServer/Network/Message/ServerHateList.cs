using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerHateList)]
    
    public class ServerHateList : SubPacket
    {
        public Dictionary<Character,uint> HateList;
        public override void Write(BinaryWriter writer)
        {
            writer.Write((uint)this.HateList.Count);
            for (int i = 0; i < 32; i++)
            {
                if (i < this.HateList.Count)
                {
                    var entry = this.HateList.Keys.ElementAt(i);
                    writer.Write(entry.Id);
                    writer.Write((byte)100);
                    writer.Write((byte)0);
                    writer.Write((ushort)0);
                }
                else
                {
                    writer.Pad(8);
                }
            }
            writer.Write((uint)0);
        }
    }
}