using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721357)]
    public class CtsErkEureka1EventGeroltInit : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                switch (this.player.Character.EurekaInfo.EurekaStep)
                {
                    case 0:
                        Scene00001();
                        break;
                    case 1:
                        Scene00002();
                        break;
                    default:
                        Scene00003();
                        break;
                }

            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001() // angry
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00002() // first magicite
        {
            var callback = (SceneResult result) =>
            {
                player.SetEurekaStep(2);
            };
            owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00003()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00004()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00005()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}