using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721369)]
    public class CtsErkEureka1Transpoint : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                Scene00002();
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001() // attune
        {
            var callback = (SceneResult result) =>
            {
                Scene00002();
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_UI, Callback: callback );
        }
        
        private void Scene00002() // teleport menu
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 2, SceneFlags.NO_DEFAULT_CAMERA, callback, 2, 2, 3 );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}