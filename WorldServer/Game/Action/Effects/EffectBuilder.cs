using WorldServer.Game.Action.Enums;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Action.Effects
{
    public static class EffectBuilder
    {
        public static Effect Damage(Action action, (float, ActionHitSeverityType) dmg, Character target,  ActionEffectResultFlag flag = ActionEffectResultFlag.None)
        {
            var attacktype = action.ActionData.AttackType.Row;
            if (attacktype == uint.MaxValue)
                attacktype = 1;
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Parameter1 = (byte)((byte)(dmg.Item2)*32),
                Parameter2 = (byte)(112 + attacktype),
                Value = (uint)dmg.Item1,
                Flags = flag,
                Type = ActionEffectType.Damage
            };
        }
        
        public static Effect Heal(Action action, (float, ActionHitSeverityType) heal, Character target, ActionEffectResultFlag flag = ActionEffectResultFlag.None)
        {
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Parameter2 = (byte)((byte)(heal.Item2)*32),
                Value = (uint)heal.Item1,
                Flags = flag,
                Type = ActionEffectType.Heal
            };
        }
        
        public static Effect Mount(Action action, uint mountId, Character target)
        {
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Parameter1 = 1,
                Value = mountId,
                Type = ActionEffectType.Mount,
                ExecuteDelay = 400,
            };
        }
        
        public static Effect StatusEffectTarget(Action action, ushort statusId, float duration, ushort param , Character target)
        {
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Parameter3 = param,
                Value = statusId,
                StatusDuration = duration,
                Type = ActionEffectType.ApplyStatusEffectTarget,
                ExecuteDelay = 0,
            };
        }
        
        public static Effect StatusEffectSource(Action action, ushort statusId, float duration, ushort param)
        {
            return new Effect
            {
                Source = action.Source,
                Target = action.Source,
                Parameter3 = param,
                Value = statusId,
                StatusDuration = duration,
                Flags = ActionEffectResultFlag.EffectOnSource,
                Type = ActionEffectType.ApplyStatusEffectTarget,
                ExecuteDelay = 0,
            };
        }
        
        public static Effect StartCombo(Action action, Character target)
        {
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Value = action.ActionId,
                Flags = ActionEffectResultFlag.EffectOnSource,
                Type = ActionEffectType.StartActionCombo,
            };
        }
        
        public static Effect ComboSucceed(Action action, Character target)
        {
            return new Effect
            {
                Source = action.Source,
                Target = target,
                Value = action.ActionId,
                Type = ActionEffectType.ComboSucceed,
            };
        }
    }
}