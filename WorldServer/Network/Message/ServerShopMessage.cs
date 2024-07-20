using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerShopMessage)]
    public class ServerShopMessage : SubPacket
    {
        public uint ShopId;
        public uint MsgType;
        public uint ItemId;
        public uint Amount;
        public uint Price;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(this.ShopId);
            writer.Write(this.MsgType);
            writer.Write((uint)3);
            writer.Write(this.ItemId);
            writer.Write(this.Amount);
            writer.Write(this.Price);
            writer.Write((uint)0);
            writer.Write((uint)0);
        }
    }
}