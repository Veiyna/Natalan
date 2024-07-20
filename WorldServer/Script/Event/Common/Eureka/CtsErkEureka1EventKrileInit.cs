using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721356)]
    public class CtsErkEureka1EventKrileInit : EventScript
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
                player.SetEurekaStep(1);
            };
            owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI | SceneFlags.INVIS_ENPC, Callback: callback );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}