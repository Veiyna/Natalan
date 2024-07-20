using WorldServer.Game.Housing;

using System.IO;
using Shared.Network;


namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerLandInfoSign)]
    public class ServerLandInfoSign : SubPacket
    {
        public Land Land;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Land.LandIdent.Marshal());
            writer.Write((ulong)0);
            writer.Write((uint)0);
            writer.Write((byte)0);
            writer.Write((byte)this.Land.HouseSize);
            writer.Write((byte)this.Land.LandType);
            writer.WriteStringLength("test", 23);
            writer.WriteStringLength("test", 193);
            writer.WriteStringLength("test", 31);
            writer.WriteStringLength("test", 7);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
        }
    }
}