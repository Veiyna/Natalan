using System;
using System.Collections.Generic;
using System.Reflection;
using Shared.SqPack;
using WorldServer.Game.Action.Effects;
using WorldServer.Game.Action.Enums;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game.Action
{
    public static class ActionManager
    {
        
        public delegate void EffectHandler(Effect effect);
        private static readonly Dictionary<ActionEffectType, EffectHandler> EffectHandlers =  new();
        
        
        public static void Initialise()
        {
            InitialiseEffectHandlers();
        }

        public static void HandleAction(Character source, uint actionId, ulong targetId, ushort sequence, ushort itemContainer, ushort itemSlot, SkillType skillType = SkillType.Normal)
        {
            Action action;
            switch (skillType)
            {
                case SkillType.EventItem:
                    action = new EventItemAction(source, 3, sequence, targetId, source.Position, skillType, actionId);
                    break;
                case SkillType.ItemAction:
                {
                    var item = GameTableManager.Items.GetRow(actionId);
                    var itemAction = item?.ItemAction.Value;
                    action = new ItemAction(source, itemAction.Type, sequence, targetId, source.Position, skillType, 0, itemAction, itemContainer, itemSlot, item);
                    break;
                }

                case SkillType.MountSkill:
                    action = new Action(source, 4, sequence, targetId, source.Position, skillType, actionId);
                    break;
                default:
                    action = new Action(source, actionId, sequence, targetId, source.Position, skillType, 0);
                    break;

            }
            BootstrapAction(action);
        }

        public static void BootstrapAction(Action action)
        {
            if (action.HasCastTime())
                action.Source.CurrentAction = action;
            action.Start();
        }


        public static void InitialiseEffectHandlers()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (MethodInfo methodInfo in type.GetMethods())
            foreach (EffectHandlerAttribute attribute in methodInfo.GetCustomAttributes<EffectHandlerAttribute>())
                EffectHandlers[attribute.ActionEffectType] = (EffectHandler)Delegate.CreateDelegate(typeof(EffectHandler), methodInfo);
        }

        public static void ExecuteEffect(Effect effect)
        {
            EffectHandler handler;
            if (!EffectHandlers.TryGetValue(effect.Type, out handler))
                return;
            handler.Invoke(effect);
        }
    }
}