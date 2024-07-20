using Shared.Game;
using Shared.Network;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class EventHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventGossip, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventGossip(WorldSession session, ClientEventGossip eventGossip)
        {
            session.Player.Event.OnGossip(eventGossip.EventId, eventGossip.ActorId);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEventEmote, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventEmote(WorldSession session, ClientEventEmote eventEmote)
        {
            session.Player.Event.OnEmote(eventEmote.EventId, eventEmote.ActorId, eventEmote.EmoteId);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEventAreaTrigger, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleOnAreaTrigger(WorldSession session, ClientEventAreaTrigger eventAreaTrigger)
        {
            session.Player.Event.OnAreaTrigger(eventAreaTrigger.EventId, eventAreaTrigger.ActorId, new WorldPosition(session.Player.Position.TerritoryId, eventAreaTrigger.Position, 0f));
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEventOutOfBounds, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventOutOfBounds(WorldSession session, ClientEventOutOfBounds eventOutOfBounds)
        {
            session.Player.Event.OnOutOfBounds(eventOutOfBounds.EventId, eventOutOfBounds.ActorId, new WorldPosition(session.Player.Position.TerritoryId, eventOutOfBounds.Position, 0f));
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEventTerritory, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventTerritory(WorldSession session, ClientEventTerritory territory)
        {
            session.Player.Event.OnTerritory(territory.EventId);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientEventSceneFinish, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventSceneFinish2, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventSceneFinish3, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventSceneFinish(WorldSession session, ClientEventSceneFinish eventSceneFinish)
        {
            session.Player.Event.OnSceneFinish(eventSceneFinish.EventId,eventSceneFinish.SceneId, eventSceneFinish.ErrorCode, eventSceneFinish.ParamCount, eventSceneFinish.Data);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventYield2, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventYield16, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventYield(WorldSession session, ClientEventYield eventYield)
        {
            session.Player.Event.OnYield(eventYield.EventId, eventYield.SceneId, eventYield.YieldId, eventYield.Params);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventYieldString8, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventYieldString16, SubPacketHandlerFlags.RequiresWorld)]
        [SubPacketHandler(SubPacketClientHandlerId.ClientEventYieldString32, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleEventYieldString(WorldSession session, ClientEventYieldString eventYield)
        {
            session.Player.Event.OnYield(eventYield.EventId, eventYield.SceneId, eventYield.YieldId, eventYield.Value);
        }
        
    }
}
