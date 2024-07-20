using Shared.Database.Datacentre.Models;
using Shared.Game;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Event
{
    public abstract class EventScript
    {
        protected Player owner;
        protected Player player => this.owner;
        protected Event Event;
        protected uint Id;
        

        public void Initialise(Player player, Event ourEvent)
        {
            owner = player;
            this.Event = ourEvent;
            Id = this.Event.Id;

        }
        
        public void Initialise(Player player, uint id)
        {
            owner = player;
            this.Id = id;
        }

        public virtual uint GetParameter(EventType type) { return 0u; }

        public virtual void OnGossip(ulong actorId) { }
        public virtual void OnEventItem(ulong actorId) { }
        public virtual void OnEmote(ulong actorId, ushort emoteId) { }
        public virtual void OnAreaTrigger(ulong actorId, WorldPosition position) { }
        public virtual void OnBNpcKill(BNpc bNpc) { }
        public virtual void OnOutOfBounds(ulong actorId, WorldPosition position) { }
        public virtual void OnEventTerritory() { }
        public virtual void OnYield(byte yieldId, uint[] data) { owner.Event.ResumeEvent(Id, yieldId); }
        public virtual void OnYield(byte yieldId, string data) { owner.Event.ResumeEvent(Id, yieldId); }
        public virtual void OnSceneFinish(ushort sceneId, SceneResult result) {}
    }
}
