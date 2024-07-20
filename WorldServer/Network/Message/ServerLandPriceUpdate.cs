using WorldServer.Game.Housing;

using System.IO;
using Shared.Network;


namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerLandPriceUpdate)]
    public class ServerLandPriceUpdate : SubPacket
    {
        public Land Land;
        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.Land.Price);
            writer.Write((uint)0);
        }
    }
}