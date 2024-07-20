using WorldServer.Game.Entity;

namespace WorldServer.Game.Action
{
    public abstract class ActionScript
    {
        protected Player owner;

        public void Initialise(Player player)
        {
            owner = player;
        }
        
        public virtual void OnStart(Action action) { }
        public virtual void OnExecute(Action action) { }
        
        public virtual void OnInterrupt(Action action) { }
        
        
    }
}