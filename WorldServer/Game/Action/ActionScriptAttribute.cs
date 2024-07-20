using System;

namespace WorldServer.Game.Action
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ActionScriptAttribute : Attribute
    {
        public uint ActionId { get; }

        public ActionScriptAttribute(uint actionId)
        {
            ActionId = actionId;
        }
    }
}