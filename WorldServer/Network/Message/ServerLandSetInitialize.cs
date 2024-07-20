using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Housing;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerLandSetInitialize)]
public class ServerLandSetInitialize : SubPacket
{
    public LandSet LandSet;
    public Player player;
    public override void Write(BinaryWriter writer)
    {
            writer.Write((ushort)0); // landId
            writer.Write(this.LandSet.WardId); // wardNum
            writer.Write(this.LandSet.TerritoryId); // territoryTypeId
            writer.Write(this.player.Character.CurrentRealmId); // worldId
            writer.Write((byte)1);
            writer.Write((byte)1);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            for (int i = 0; i < 30; i++)
            {
                var land = this.LandSet.Lands[i];
                writer.Write((byte)land.HouseSize);
                writer.Write((byte)land.HouseState);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((uint)0);
                writer.Write((uint)0);
                writer.Write((uint)0);
                for (int j = 0; j < 8; j++)
                {
                    writer.Write((ushort)0);
                }
                for (int j = 0; j < 8; j++)
                {
                    writer.Write((byte)0);
                }
            }
        }
}