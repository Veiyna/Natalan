using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721034)]
    public class CmnDefMinstrel : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                Scene00001();
            };
            this.owner.Event.NewScene(Id, 0, SceneFlags.HIDE_HOTBAR , Callback: callback);
        
        }
        
        private void Scene00001()
        {
            var callback = (SceneResult result) =>
            {
                
            };
            this.owner.Event.NewScene(Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback);
        
        }
        
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
        
    }
}