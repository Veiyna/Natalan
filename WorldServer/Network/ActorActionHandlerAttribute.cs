using System;

namespace WorldServer.Network
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ActorActionHandlerAttribute : Attribute
    {
        public ActorActionClient Action { get; }

        public ActorActionHandlerAttribute(ActorActionClient action)
        {
            Action = action;
        }
    }
}
