using System.IO;
using Shared.Network;
using WorldServer.Game.Housing.Enums;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerLandAvailability)]
public class ServerLandAvailability : SubPacket
{
    public LandSellMode SellMode;
    public LandAvailableTo AvailableTo;
    public LandLotteryStatus LotteryStatus;
    public LandLotteryPlayerResult LotteryPlayerResult;
    public override void Write(BinaryWriter writer)
    {
            writer.Write((byte)this.SellMode);
            writer.Write((byte)this.AvailableTo);
            writer.Write((byte)this.LotteryStatus);
            writer.Write((byte)this.LotteryPlayerResult);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
            writer.Write((uint)0);
        }
}