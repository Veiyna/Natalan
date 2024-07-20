using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Shared.SqPack;
using WorldServer.Game.Action;
using WorldServer.Game.Event;
using WorldServer.Game.Map;

namespace WorldServer.Script
{
    public static class ScriptManager
    {
        private static ReadOnlyDictionary<uint, Type> eventScripts;
        private static ReadOnlyDictionary<uint, Type> actionScripts;
        private static ReadOnlyDictionary<uint, Type> instanceScripts;

        public static void Initialise()
        {
            InitialiseEventScripts();
            InitialiseActionScripts();
            InitialiseInstanceScripts();
        }

        private static void InitialiseEventScripts()
        {
            var scripts = new Dictionary<uint, Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                foreach (EventScriptAttribute attribute in type.GetCustomAttributes<EventScriptAttribute>())
                    scripts.Add(attribute.EventId, type);

            eventScripts = new ReadOnlyDictionary<uint, Type>(scripts);
        }
        
        private static void InitialiseActionScripts()
        {
            var scripts = new Dictionary<uint, Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (ActionScriptAttribute attribute in type.GetCustomAttributes<ActionScriptAttribute>())
                scripts.Add(attribute.ActionId, type);

            actionScripts = new ReadOnlyDictionary<uint, Type>(scripts);
        }
        
        private static void InitialiseInstanceScripts()
        {
            var scripts = new Dictionary<uint, Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            foreach (InstanceContentScriptAttribute attribute in type.GetCustomAttributes<InstanceContentScriptAttribute>())
                scripts.Add(attribute.InstanceId, type);

            instanceScripts = new ReadOnlyDictionary<uint, Type>(scripts);
        }

        public static bool IsValidEvent(uint eventId)
        {
            switch ((EventHiType)(eventId >> 16))
            {
                case EventHiType.Quest:
                    return GameTableManager.Quests.HasRow((int)eventId);
                case EventHiType.Warp:
                    return GameTableManager.Warp.HasRow((int)eventId);
                case EventHiType.GilShop:
                    return GameTableManager.GilShop.HasRow((int)eventId);
                case EventHiType.Aetheryte:
                    return GameTableManager.Aetheryte.HasRow((int)eventId & 0xFFFF);
                case EventHiType.Opening:
                    return GameTableManager.OpeningEvents.HasRow((int)eventId);
                case EventHiType.GCShop:
                    return GameTableManager.GCShop.HasRow((int)eventId);
                case EventHiType.DefaultGossip:
                    return GameTableManager.DefaultTalk.HasRow((int)eventId);
                case EventHiType.SwitchGossip:
                    return GameTableManager.SwitchTalk.HasRow((int)eventId);
                case EventHiType.CustomGossip:
                    return GameTableManager.CustomTalk.HasRow((int)eventId);
                case EventHiType.ChocoboTaxiStand:
                    return GameTableManager.ChocoboTaxiStand.HasRow((int)eventId);
                case EventHiType.SpecialShop:
                    return GameTableManager.SpecialShop.HasRow((int)eventId);
                case EventHiType.PreHandler:
                    return GameTableManager.PreHandler.HasRow((int)eventId);
                case EventHiType.InstanceContentGuide:
                    return GameTableManager.PreHandler.HasRow((int)eventId);
                case EventHiType.GuildLeveAssignment:
                    return GameTableManager.GuildleveAssignment.HasRow((int)eventId);
                case EventHiType.HousingAethernet:
                    return GameTableManager.HousingAethernet.HasRow((int)eventId);
                default:
                    return false;
            }
        }

        public static bool IsValidEventScene(uint eventId, ushort sceneId)
        {
            // TODO
            return true;
        }

        public static EventScript NewEventScript(uint eventId)
        {
            if (!eventScripts.TryGetValue(eventId, out var scriptType) && !eventScripts.TryGetValue(eventId & 0xFFFF0000, out scriptType))
            {
                #if DEBUG
                    Console.WriteLine($"Event {eventId} has no assigned script!");
                #endif
                return null;
            }

            return (EventScript)Activator.CreateInstance(scriptType);
        }
        
        public static ActionScript NewActionScript(uint actionId)
        {
            if (!actionScripts.TryGetValue(actionId, out var scriptType))
            {
                #if DEBUG
                Console.WriteLine($"Action {actionId} has no assigned script!");
                #endif
                return null;
            }

            return (ActionScript)Activator.CreateInstance(scriptType);
        }
        
        public static InstanceContentScript NewInstanceContentScript(uint instanceId)
        {
            if (!instanceScripts.TryGetValue(instanceId, out var scriptType))
            {
                return null;
            }
            return (InstanceContentScript)Activator.CreateInstance(scriptType);
        }
        
        
        
    }
}
