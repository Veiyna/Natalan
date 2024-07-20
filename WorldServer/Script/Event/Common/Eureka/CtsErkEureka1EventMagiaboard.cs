using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721360)]
    public class CtsErkEureka1EventMagiaboard : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                switch (this.player.Character.EurekaInfo.EurekaStep)
                {
                    case 2:
                        Scene00001();
                        break;
                    case > 2:
                        Scene00002();
                        break;
                    default:
                        Scene00003();
                        break;
                }
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001() // magia board obtained
        {
            var callback = (SceneResult result) =>
            {
                this.owner.SetEurekaStep(3);
            };
            owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback);
            this.owner.Event.StopEvent(this.Id);
        }
        
        private void Scene00002() // magia board open
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback);
        }
        
        private void Scene00003() // no magia board
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback);
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}