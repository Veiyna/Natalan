using System;
using System.Collections.Generic;
using System.Reflection;
using WorldServer.Data;
using WorldServer.Game.StatusEffect.Enums;
using WorldServer.Game.StatusEffect.StatusEffectTrigger;

namespace WorldServer.Game.StatusEffect
{
    public static class StatusEffectManager
    {
        public delegate void StatusExecuteHandler(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute);
        private static readonly Dictionary<StatusEffectType, StatusExecuteHandler> EffectHandlers =  new();
        
        public static void Initialise()
        {
            InitialiseEffectHandlers();
        }
        public static void InitialiseEffectHandlers()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (MethodInfo methodInfo in type.GetMethods())
            foreach (StatusExecuteAttribute attribute in methodInfo.GetCustomAttributes<StatusExecuteAttribute>())
                EffectHandlers[attribute.StatusEffectType] = (StatusExecuteHandler)Delegate.CreateDelegate(typeof(StatusExecuteHandler), methodInfo);
        }
        
        public static void ExecuteEffect(StatusEffect statusEffect, StatusEffectData.StatusExecute statusExecute)
        {
            StatusExecuteHandler handler;
            if (!EffectHandlers.TryGetValue(statusExecute.Type, out handler))
                return;
            Console.WriteLine($"Executing Status Trigger: {statusExecute.Trigger.ToString()} Type: {statusExecute.Type.ToString()}");
            handler.Invoke(statusEffect, statusExecute);
        }
    }
}