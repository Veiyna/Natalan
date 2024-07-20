using Lumina.Data.Parsing.Layer;
using Shared.Network;
using Shared.SqPack;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ZoneLineHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientZoneLine)]
        public static void HandleZoneLine(WorldSession session, ClientZoneLine subPacket)
        {
            var zoneLine = GameTableManager.GetLGBEntry(LayerEntryType.ExitRange,subPacket.ZoneLine).Item2;
            if (zoneLine != null)
            {
                var exitRange = (LayerCommon.ExitRangeInstanceObject)zoneLine.Value.Object;
                session.Player.TeleportToPopRange(exitRange.DestInstanceId);
            }
        }
    }
}