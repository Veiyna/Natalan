using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerHateList)]
    
public class ServerHateRank : SubPacket
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
                writer.Write(this.HateList.GetValueOrDefault(entry));
            }
            else
            {
                writer.Pad(8);
            }
        }
        writer.Write((uint)0);
    }
}