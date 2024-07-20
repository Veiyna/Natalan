using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721351)]
    public class CtsErkEureka1EventKrile : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                switch (this.player.Character.EurekaInfo.EurekaStep)
                {
                    case 1:
                        Scene00019();
                        break;
                    case 2:
                        Scene00020();
                        break;
                    case 3:
                        Scene00021();
                        break;
                }


            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00019()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00020()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00021()
        {
            var callback = (SceneResult result) =>
            {
                this.owner.SetEurekaStep(4);
            };
            owner.Event.NewScene( Id, 21, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI | SceneFlags.INVIS_ENPC, Callback: callback );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}