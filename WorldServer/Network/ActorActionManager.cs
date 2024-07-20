﻿using System;
using System.Collections.Generic;
using System.Reflection;
using WorldServer.Network.Message;

namespace WorldServer.Network
{
    public static class ActorActionManager
    {
        public delegate void ActorActionHandler(WorldSession session, ClientActorAction actorAction);

        private static readonly Dictionary<ActorActionClient, ActorActionHandler> actorActionHandlers = new();

        public static void Initialise()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var attribute in methodInfo.GetCustomAttributes<ActorActionHandlerAttribute>())
                        actorActionHandlers[attribute.Action] = (ActorActionHandler)Delegate.CreateDelegate(typeof(ActorActionHandler), methodInfo);
        }

        public static void Invoke(WorldSession session, ClientActorAction actorAction)
        {
            ActorActionHandler handler;
            if (!actorActionHandlers.TryGetValue(actorAction.Action, out handler))
                return;

            handler.Invoke(session, actorAction);
        }
    }
}
