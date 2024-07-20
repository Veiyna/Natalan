using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(0x1b0000)]
    public class SpecialShop : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}