using System.IO;
using Shared.Network;
using WorldServer.Game.Housing;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerLandSetMap)]
public class ServerLandSetMap : SubPacket
{
    public LandSet LandSet;
    public override void Write(BinaryWriter writer)
    {
        writer.Write((byte)1);
        writer.Write((byte)2);
        writer.Write((byte)0);
        for (var i = 0; i < 30; i++)
        {
            var land = this.LandSet.Lands[i];
            writer.Write((byte)land.HouseState);
            writer.Write((byte)land.HouseSize);
            writer.Write((byte)0);
        }
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
    }
        
}