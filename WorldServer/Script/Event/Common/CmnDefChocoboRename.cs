using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721030)]
    public class CmnDefChocoboRename : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            owner.Event.NewScene(Id,0, (SceneFlags)8192);
        }
        
        
        public override void OnYield(byte yieldId, string data)
        {
            this.owner.SetCompanionName(data);
            this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
        }
    }
}