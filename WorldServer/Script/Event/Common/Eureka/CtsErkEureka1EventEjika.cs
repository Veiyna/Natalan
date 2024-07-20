using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721368)]
    public class CtsErkEureka1EventEjika : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                Scene00001();
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback);
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}