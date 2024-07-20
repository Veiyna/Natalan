using Shared.Network;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler;

public static class TradeHandler
{
    [SubPacketHandler(SubPacketClientHandlerId.ClientTrade)]
    public static void HandleTrade(WorldSession session, ClientTrade tradeInfo)
    {
        if (tradeInfo.Type is 1)
        {
            var target = MapManager.FindPlayer(tradeInfo.ActorId);
            session.Send(new ServerTrade
            {
                Type = 2
            });
        
            
            session.Send(new ServerTrade
            {
                Type = 784,
                ActorId = session.Player.Character.ActorId
            });
        
            session.Send(target.Character.ActorId, session.Player.Character.ActorId,new ServerTrade
            {
                Type = 784,
                ActorId = target.Character.ActorId
            });
        } 
        else if (tradeInfo.Type == 7)
        {
            session.Send(new ServerTrade
            {
                Type = 7
            });
        }

        
        
    }
}