using System;
using WorldServer.Game.StatusEffect.Enums;

namespace WorldServer.Game.StatusEffect.StatusEffectTrigger
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StatusExecuteAttribute : Attribute
    {
        public StatusEffectType StatusEffectType { get; }

        public StatusExecuteAttribute(StatusEffectType statusEffectType)
        {
            this.StatusEffectType = statusEffectType;
        }
    }
}
