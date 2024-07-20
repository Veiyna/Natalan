using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721223)]
    public class CmnDefBeginnerGuide : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            owner.Event.NewScene(this.Id,0, (SceneFlags)8192);
        }
    }
}