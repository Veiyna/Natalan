using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721361)]
    public class CtsErkEureka1EventMagiaboardGuide : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                if (player.Character.EurekaInfo.EurekaStep > 2)
                {
                    Scene00002();
                }
                else
                {
                    Scene00001();
                }
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001() // guide without magiaboard
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00002() // guide with magiaboard
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, callback, 2, 2, 3 );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}