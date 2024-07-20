using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(0x000B0000)]
    public class SmallTalk : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            owner.Event.NewScene(Id,0, (SceneFlags)8192);
        }
    }
}