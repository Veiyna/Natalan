using Shared.SqPack;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(0x320000)]
    public class TopicSelect : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }

        private void Scene00000()
        {
            owner.Event.NewScene(Id,0, (SceneFlags)8192, Callback:Redirect); 
        }

        private void Redirect(SceneResult result)
        {
            var topicSelectEntry = GameTableManager.TopicSelect.GetRow(Id);
            if (result.param2 == ushort.MaxValue) return;
            if (topicSelectEntry == null) return;

            var newEventId = topicSelectEntry.Shop[result.param2];
            this.owner.Event.NewEvent(newEventId, EventType.Nest, this.Event.ActorId);
            var activeEvent = this.owner.Event.GetEvent(newEventId);
            activeEvent?.Script.OnGossip(0);


        }
    }
}