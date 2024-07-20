using System;
using System.Diagnostics;
using System.IO.Enumeration;
using WorldServer.Script;

namespace WorldServer.Game.Event
{
    public class Event
    {
        public class Scene
        {
            public ushort Id { get; }
            public SceneFlags Flags { get; }
            public bool IsComplete { get; set; }
            
            public Action<SceneResult> Callback;

            public Scene(ushort id, SceneFlags flags)
            {
                Id    = id;
                Flags = flags;
            }
        }

        private EventManager EventManager { get; init; }
        public uint Id { get; }
        public EventType Type { get; }
        public ulong ActorId { get; }
        public uint Parameter { get; }

        public EventScript Script { get; }
        public Scene ActiveScene { get; private set; }
        
        

        public Event(uint id, EventType type, ulong actorId, uint param)
        {
            Id        = id;
            Type      = type;
            ActorId   = actorId;
            Script    = ScriptManager.NewEventScript(id);
            Parameter = param;
        }

        public void SceneNew(ushort sceneId, SceneFlags flags)
        {
            ActiveScene = new Scene(sceneId, flags);
        }

        public void SceneFinish(byte errorCode, byte paramCount, params uint[] data)

        {
            Debug.Assert(!ActiveScene.IsComplete);
            ActiveScene.IsComplete = true;
            
            Script?.OnSceneFinish(ActiveScene.Id, new SceneResult(errorCode, paramCount, data));
            ActiveScene.Callback?.Invoke(new SceneResult(errorCode, paramCount, data));
        }
    }
}
