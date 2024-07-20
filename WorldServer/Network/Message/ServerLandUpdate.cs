using System.IO;
using Shared.Network;
using WorldServer.Game.Housing;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerLandUpdate)]
public class ServerLandUpdate : SubPacket
{
    public Land Land;
    public override void Write(BinaryWriter writer)
    {
            writer.Write(this.Land.LandIdent.Marshal());
            writer.Write((byte)Land.HouseSize);
            writer.Write((byte)Land.HouseState);
            writer.Write((byte)1);
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