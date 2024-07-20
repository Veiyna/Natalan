using WorldServer.Game.Action.Enums;

namespace WorldServer.Game.Action.Effects
{
    public static class EffectHandler
    {
        [EffectHandler(ActionEffectType.Damage)]
        public static void HandleDamage(Effect effect)
        {
            effect.Target.OnActionHostile(effect.Source);
            effect.Target.TakeDamage(effect.Value);
        }
        
        [EffectHandler(ActionEffectType.Heal)]
        public static void HandleHeal(Effect effect)
        {
            effect.Target.Heal(effect.Value);
        }
        
        [EffectHandler(ActionEffectType.Mount)]
        public static void HandleMount(Effect effect)
        {
            if (effect.Target.IsPlayer)
                effect.Target.ToPlayer.Mount(effect.Value);
        }
        
        [EffectHandler(ActionEffectType.ApplyStatusEffectTarget)]
        public static void HandleStatusEffectTarget(Effect effect)
        {
            foreach (var statusEffect in effect.Target.StatusEffects)
            {
                if (statusEffect.StatusId == effect.Value && statusEffect.Source == effect.Source)
                {
                    statusEffect.Duration = effect.StatusDuration;
                    effect.Target.SendStatusEffects();
                    return;
                }
            }
            effect.Target.AddStatusEffect(effect.Value, effect.Source, effect.Parameter3, effect.StatusDuration);
            
        }
    }
}