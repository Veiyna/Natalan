using WorldServer.Game.Entity;
using WorldServer.Game.Map;

namespace WorldServer.Game.Event
{
    public abstract class InstanceContentScript
    {
        protected InstanceContent InstanceContent;
        protected InstanceContent instance => this.InstanceContent;
        

        public void Initialise(InstanceContent instanceContent)
        {
            this.InstanceContent = instanceContent;

        }
        
        public virtual void OnInit() { }
        public virtual void OnGossip( Player player, EventObject eventObject, Event Event) { }
        public virtual void OnBNpcKill(BNpc bNpc) { }
        public virtual void OnLeaveTerritory(Player player) { }
    }
}