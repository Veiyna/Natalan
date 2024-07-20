using Shared.Game;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
    [EventScript(68555)]
    public class SubCts051 : EventScript
    {
        
        private static class Data
        {

            public const byte SEQ_0 = 0;
            public const byte SEQ_1 = 1;
            public const uint SEQ_FINISH = 255;

            public const uint ACTOR0 = 1003063;
        }
        public override void OnGossip(ulong actorId)
        {
            var callback = (SceneResult result) =>
            {
                if (result.param2 == 1)
                {
                    player.SetMasterUnlock((ushort)UnlockEntry.Return, true);
                    player.FinishQuest( Id );
                }
            };
            owner.Event.NewScene(this.Id,0, SceneFlags.NONE, Callback: callback);
        }
        
    }
}