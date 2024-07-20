using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Network;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerPlayerStats)]
public class ServerPlayerStats : SubPacket
{
    public Dictionary<BaseParam, Stat> Stats { get; set; }

    public override void Write(BinaryWriter writer)
    {
        uint[] stats = new uint[GameTableManager.BaseParam.Max(row => row.PacketIndex)+1];
        foreach (var stat in Stats.Values)
        {
            stats[stat.BaseParam.PacketIndex] = (uint)stat.Value;
        }

        foreach (var stat in stats)
        {
            writer.Write(stat);
        }

    }
}