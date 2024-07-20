using WorldServer.Data;
using WorldServer.Game.Action.Enums;
using WorldServer.Game.Calc;
using WorldServer.Game.StatusEffect.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.StatusEffect.StatusEffectTrigger
{
    public static class StatusExecuteHandler
    {
        [StatusExecute(StatusEffectType.GaugeToggle)]
        public static void HandleGaugeTemporary(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute)
        {
            if (statusEffect.Target.IsPlayer)
            {
                var player = statusEffect.Target.ToPlayer;
                if(player.JobGauge[4] == 0)
                    player.JobGauge[4] = 1;
                else
                {
                    player.JobGauge[4] = 0;
                }
                statusEffect.Target.ToPlayer.SendJobGauge();
            }
        }
        
        [StatusExecute(StatusEffectType.MPRestore)]
        public static void HandleMPRestore(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute)
        {
            statusEffect.Target.RestoreMP((ushort)statusExecute.Value);
        }
        
        [StatusExecute(StatusEffectType.Damage)]
        public static void HandleDamage(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute)
        {
            var damage = CalcManager.SimplePotencyToValue(statusExecute.Value);
            statusEffect.Target.TakeDamage((ushort)damage);
            statusEffect.Target.SendMessageToVisible(new ServerActorAction
            {
                Action = ActorActionServer.HPFloatingText,
                Parameter2 = (uint)ActionEffectType.Damage,
                Parameter3 = damage
            },true);
        }
        
        [StatusExecute(StatusEffectType.Heal)]
        public static void HandleHeal(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute)
        {
            var heal = CalcManager.SimplePotencyToValue(statusExecute.Value);
            statusEffect.Target.Heal((ushort)heal);

            switch (statusExecute.Trigger) 
            {
                case StatusEffectTriggerType.Tick:
                    statusEffect.Target.SendMessageToVisible(new ServerActorAction
                    {
                        Action = ActorActionServer.HPFloatingText,
                        Parameter2 = (uint)ActionEffectType.Heal,
                        Parameter3 = heal
                    },true);

                    break;
                case StatusEffectTriggerType.Apply:
                case StatusEffectTriggerType.Remove:
                    statusEffect.Target.SendMessageToVisible(new ServerActorAction
                    {
                        Action = ActorActionServer.HPFloatingText2,
                        Parameter1 = statusEffect.StatusId,
                        Parameter2 = heal,
                        Parameter3 = statusEffect.Target.Id,
                    },true);

                    break;
                    
                    
            }

        }
    }
}