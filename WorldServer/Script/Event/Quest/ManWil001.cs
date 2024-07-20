using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
    [EventScript(66130)]
    public class ManWil001 : EventScript
    {
        
        private static class Data
        {

            public const byte SEQ_0 = 0;
            public const byte SEQ_1 = 1;
            public const uint SEQ_FINISH = 255;

            public const uint ACTOR0 = 1001028;
            public const uint ACTOR1 = 1002732;
            public const uint ACTOR2 = 1002697;
            public const uint CUT_EVENT = 202;
            public const uint EOBJECT0 = 2001679;
            public const uint EOBJECT1 = 2001680;
            public const uint EVENT_ACTION_SEARCH = 1;
            public const uint LOC_ACTOR0 = 1002732;
            public const uint LOC_POS_ACTOR0 = 4107186;
            public const uint OPENING_EVENT_HANDLER = 1245185;
            public const uint POPRANGE0 = 4127803;
            public const uint TERRITORYTYPE0 = 181;
        }
        public override void OnGossip(ulong actorId)
        {
            var callback = (SceneResult result) =>
            {
                this.owner.Event.NewScene(this.Id, 1, 0);
            };
            owner.Event.NewScene(this.Id,0, (SceneFlags)8192, Callback: callback);
        }
        
    }
}