using System;
using WorldServer.Game.Action.Enums;

namespace WorldServer.Game.Action.Effects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EffectHandlerAttribute : Attribute
    {
        public ActionEffectType ActionEffectType { get; }

        public EffectHandlerAttribute(ActionEffectType actionEffectType)
        {
            this.ActionEffectType = actionEffectType;
        }
    }
}