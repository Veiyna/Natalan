using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(720997)]
    public class ComDefFreeCompanyCrest : EventScript
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
        
        public override void OnYield(byte yieldId, uint[] data)
        {
            this.owner.Event.ResumeEvent(this.Id,yieldId);
        }
        
    }
}