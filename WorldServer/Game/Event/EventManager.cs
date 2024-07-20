using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lumina.Data;
using Lumina.Data.Parsing.Layer;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network.Message;
using WorldServer.Script;

namespace WorldServer.Game.Event
{
    public class EventManager
    {
        private readonly Player owner;
        public Dictionary<uint,Event> Events = new();

        public EventManager(Player player)
        {
            owner = player;
        }

        /// <summary>
        /// Initialise a new client event.
        /// </summary>
        public void NewEvent(uint eventId, EventType eventType, ulong actorId, uint param = 0)
        {
            #if DEBUG
                Console.WriteLine($"{owner.Character.Name} starting event {eventId} with type {eventType}.");
            #endif

            if (!ScriptManager.IsValidEvent(eventId))
                Console.WriteLine($"Invalid event {eventId}!");

            if (this.Events.Any() && eventType != EventType.Nest)
                Console.WriteLine($"Failed to start event {eventId}, an existing event is still in progress!");

            
            var newEvent = new Event(eventId, eventType, actorId, param);
            this.Events.Add(eventId, newEvent);
            newEvent.Script?.Initialise(owner, newEvent);

            owner.Session.Send(new ServerEventStart
            {
                Event = newEvent,
                State = 0
            });

            // client softlocks waiting for event, to prevent this complete unhandled events instantly
            /*if (newEvent.Script == null)
            {
                #if DEBUG
                    Console.WriteLine($"No assigned script for event {eventId:X4}!");
                #endif
                StopEvent(eventId);
            }*/
        }

        public void StopAllEvents()
        {
            foreach (var eventId in this.Events.Keys)
            {
                StopEvent(eventId);
            }
        }
        
        public void StopEvent(uint eventId)
        {
            #if DEBUG
                Console.WriteLine($"{owner.Character.Name} stopping event {eventId}.");
            #endif

            var activeEvent = GetEvent(eventId);

            if (activeEvent == null)
            {
                Console.WriteLine($"Failed to stop event {eventId}. Event doesn't exist.");
                return;
            }
            
            if (activeEvent.ActiveScene != null && !activeEvent.ActiveScene.IsComplete)
                activeEvent.SceneFinish(0, 0 ,0 ,0);

            if (this.Events.Count > 1 && activeEvent.Type != EventType.Nest)
                return;
            



            owner.Session.Send(new ServerEventStop
            {
                Event = activeEvent,
                State = 1
            });

            this.Events.Remove(activeEvent.Id);
            
            if(activeEvent.Type == EventType.Nest)
                StopAllEvents();

            // TODO: handle player states properly
            this.owner.SendStateFlags();
            this.owner.RemoveOnlineStatus(OnlineStatus.ViewingCutscene);
        }

        public void ResumeEvent(uint eventId, byte yieldId, params uint[] data)
        {
            var activeEvent = this.Events[eventId];
            this.owner.Session.Send(new ServerEventYield
            {
                Event = activeEvent,
                ResumeId = yieldId,
                Data = data
            });
        }
        
        /// <summary>
        /// Starts a new scene for the current client event.
        /// </summary>
        public void NewScene(uint eventId,ushort sceneId, SceneFlags flags, Action<SceneResult> Callback = null, params uint[] data)
        {
            #if DEBUG
                Console.WriteLine($"{owner.Character.Name} starting scene {sceneId} for event {eventId:X4}.");
            #endif
            var activeEvent = GetEvent(eventId);
            if (activeEvent == null)
                throw new EventStateException($"Scene {sceneId} has no existing event in progress!");

            if (!ScriptManager.IsValidEventScene(activeEvent.Id, sceneId))
                throw new ArgumentException($"Invalid scene {sceneId} for event {activeEvent.Id:X4}!", nameof(sceneId));
            
            if(flags.HasFlag(SceneFlags.CONDITION_CUTSCENE))
                this.owner.AddOnlineStatus(OnlineStatus.ViewingCutscene);

            activeEvent.SceneNew(sceneId, flags);
            activeEvent.ActiveScene.Callback = Callback;

            owner.Session.Send(new ServerEventSceneStart
            {
                Event = activeEvent,
                Flags = flags,
                ParamCount = (uint)data.Length,
                Params = data
            });
        }
        
        public Event GetEvent(uint eventId)
        {
            Event Event;
            this.Events.TryGetValue(eventId, out Event);
            return Event;
        }

        public void OnSceneFinish(uint eventId, ushort sceneId, byte errorCode, byte paramCount, uint[] data)
        {
            var activeEvent = this.Events[eventId];
            #if DEBUG
                Console.WriteLine($"{owner.Character.Name} finishing scene {activeEvent.ActiveScene.Id} for event {activeEvent.Id}.");
            #endif

            if (activeEvent.ActiveScene.Id != sceneId)
                throw new EventStateException($"Scene {sceneId} doesn't match what the server expected!");

            if (activeEvent.ActiveScene.IsComplete)
                throw new EventStateException($"Scene {sceneId} is already complete!");
            
            activeEvent.SceneFinish(errorCode, paramCount, data );
            
            if(activeEvent.ActiveScene.IsComplete)
                StopEvent(eventId);
                
            this.owner.UpdateQuests();
        }

        public void OnGossip(uint eventId, ulong actorId)
        {
            NewEvent(eventId, EventType.Gossip, actorId);
            var activeEvent = GetEvent(eventId);
            var eobj = this.owner.Map.GetEObj((uint)actorId);
            if (eobj != null)
            {
                if (this.owner.Map.GetType() == typeof(InstanceContent))
                {
                    var instance = (InstanceContent)owner.Map;
                    instance.Script?.OnGossip(this.owner, eobj, activeEvent);
                }
            }
            actorId = this.owner.Event.EventActorToRealActor((uint)actorId);
            activeEvent?.Script?.OnGossip(actorId);

            CheckEvent(eventId);
        }

        public void OnEmote(uint eventId, ulong actorId, ushort emoteId)
        {
            NewEvent(eventId, EventType.Emote, actorId, emoteId);
            var activeEvent = GetEvent(eventId);
            actorId = this.owner.Event.EventActorToRealActor((uint)actorId);
            activeEvent?.Script?.OnEmote(actorId, emoteId);
            CheckEvent(eventId);
        }

        public void OnAreaTrigger(uint eventId, uint actorId, WorldPosition position)
        {
            NewEvent(eventId, EventType.WithinRange, owner.Character.ActorId, actorId);
            var activeEvent = GetEvent(eventId);
            activeEvent?.Script?.OnAreaTrigger(actorId, position);
            CheckEvent(eventId);
        }

        public void OnOutOfBounds(uint eventId, uint actorId, WorldPosition position)
        {
            NewEvent(eventId, EventType.OutOfBounds, owner.Character.ActorId, actorId);
            var activeEvent = GetEvent(eventId);
            activeEvent?.Script?.OnOutOfBounds(actorId, position);
            CheckEvent(eventId);
        }

        public void OnTerritory(uint eventId)
        {
            NewEvent(eventId, EventType.Territory, owner.Character.ActorId, this.owner.Position.TerritoryId);
            var activeEvent = GetEvent(eventId);
            //owner.Event.NewScene(eventId, 2, (SceneFlags)206808065, null, 0x9);
            activeEvent?.Script?.OnEventTerritory();
            
            
            CheckEvent(eventId);
        }

        
        //TODO: Better way to handle this? EventScripts are made to be used with events but this is not the case here
        public void OnBNpcKill(BNpc bnpc)
        {
            foreach (var quest in this.owner.Character.Quests)
            {
                var eventId = (uint)(quest.QuestId + 65536);
                var script = ScriptManager.NewEventScript((uint)(quest.QuestId + 65536));
                script.Initialise(this.owner, eventId);
                script.OnBNpcKill(bnpc);
                CheckEvent(eventId);
                
            } 
        }
        
        public void OnYield(uint eventId, ushort sceneId, byte yieldId, uint[] data)
        {
            var activeEvent = GetEvent(eventId);
            activeEvent?.Script.OnYield(yieldId, data);
            CheckEvent(eventId);
        }
        
        public void OnYield(uint eventId, ushort sceneId, byte yieldId, string data)
        {
            var activeEvent = GetEvent(eventId);
            activeEvent?.Script.OnYield(yieldId, data);
            CheckEvent(eventId);
        }

        public void OnEventItem(uint eventId, uint actorId)
        {
            NewEvent(eventId, EventType.ActionResult, owner.Character.ActorId, actorId);
            var activeEvent = GetEvent(eventId);
            activeEvent?.Script.OnEventItem(actorId);
            CheckEvent(eventId);
        }



        public uint EventActorToRealActor(uint eventActorId)
        {
            var levelentry = GameTableManager.Level.GetRow(eventActorId);
            LayerCommon.InstanceObject? eNpc = GameTableManager.GetLGBEntry(LayerEntryType.EventNPC, eventActorId).Item2;
            LayerCommon.InstanceObject? eObj = GameTableManager.GetLGBEntry(LayerEntryType.EventObject, eventActorId).Item2;
            if (levelentry != null)
                return levelentry.Object;

            if (eNpc != null)
            {
                dynamic eNpcDynamic = eNpc;
                return eNpcDynamic.Object.ParentData.ParentData.BaseId;
            }
            
            if (eObj != null)
            {
                dynamic eObjDynamic = eObj;
                return eObjDynamic.Object.ParentData.BaseId;
            }


            return eventActorId;


            


        }

        public void CheckEvent(uint eventId)
        {
            //TODO: Update only quests that have changed
            this.owner.UpdateQuests();
            if(GetEvent(eventId)?.ActiveScene == null)
                StopEvent(eventId);
        }
    }
}
