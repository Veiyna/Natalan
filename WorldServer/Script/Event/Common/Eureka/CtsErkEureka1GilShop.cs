using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721344)]
    public class CtsErkEureka1GilShop : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                if(player.Character.EurekaInfo.EurekaStep > 3)
                    Scene00002();
                else
                    Scene00001();
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
        
        private void Scene00002()
        {
            var callback = (SceneResult result) =>
            {
                Scene00003();
            };
            owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, callback);
        }
        
        private void Scene00003()
        {
            var callback = (SceneResult result) =>
            {
                Scene00004();
            };
            owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00004()
        {
            var callback = (SceneResult result) =>
            {
                uint newEventId = 262908;
                this.owner.Event.NewEvent(newEventId, EventType.Nest, this.Event.ActorId);
                var activeEvent = this.owner.Event.GetEvent(newEventId);
                activeEvent?.Script.OnGossip(0);
            };
            owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}