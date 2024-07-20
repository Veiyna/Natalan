using Shared.Network;
using WorldServer.Game.Housing;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler;

public static class HousingHandler
{
    [SubPacketHandler(SubPacketClientHandlerId.ClientHousingBuildPreset, SubPacketHandlerFlags.RequiresWorld)]
    public static void HandleHousingBuildPreset(WorldSession session, ClientHousingBuildPreset paHousingBuildPreset)
    {
        HousingManager.BuildEstate(session.Player, paHousingBuildPreset.PlotNumber, paHousingBuildPreset.ItemId);
    } 
}