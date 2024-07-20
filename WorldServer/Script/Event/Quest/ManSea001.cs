using System.Configuration;
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
    [EventScript(65643)]
    public class ManSea001 : EventScript
    {
        
        private static class Data
        {

            public const byte SEQ_0 = 0;
            public const byte SEQ_1 = 1;
            public const byte SEQ_FINISH = 255;

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
        
         

        private void Scene00000()
        {
            owner.Event.NewScene(this.Id,0, SceneFlags.HIDE_HOTBAR, Callback:result =>
            {
                {
                    if (result.param2 == 1)
                    {
                        this.owner.Character.OpeningSequence = 2;
                        Scene00001();
                    }
                }
            });
        }
        
        private void Scene00001()
        {
            owner.Event.NewScene(this.Id, 1, SceneFlags.HIDE_HOTBAR | SceneFlags.SET_BASE, Callback: Scene00002);
        }

        
        private void Scene00002(SceneResult result)
        {
            this.owner.UpdateQuest(Id, Data.SEQ_1);
            owner.Event.NewScene(this.Id, 2, SceneFlags.NONE, Callback: Scene00003);
        }
        
        private void Scene00003(SceneResult result)
        {
            owner.Event.NewScene(this.Id, 3, SceneFlags.NONE, Callback: _ =>
            {
                this.owner.Event.NewEvent(Data.OPENING_EVENT_HANDLER, EventType.Nest, this.Event.ActorId);
                this.owner.Event.NewScene(Data.OPENING_EVENT_HANDLER, 0x1E, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA);
            });
        }

        private void Scene00005()
        {
            this.owner.Event.NewScene(Id, 5, SceneFlags.HIDE_HOTBAR, Callback: Scene00006);
        }
        
        private void Scene00006(SceneResult result)
        {
            this.owner.Event.NewScene(Id, 6, SceneFlags.INVIS_OTHER_PC, Callback: result =>
            {
                if (result.param1 == 512)
                {
                    this.owner.UpdateQuest(Id, Data.SEQ_FINISH);
                    this.owner.TeleportTo(new WorldPosition(181, new Vector3(10.8f, 40, 15.2f ), 2));
                }
            });
        }

        private void Scene00011()
        {
            this.owner.Event.NewScene(Id, 11, (SceneFlags)0x2c02, Callback: Scene00012);
        }
        
        private void Scene00012(SceneResult result)
        {
            this.owner.Event.NewScene(Id, 12, SceneFlags.INVIS_OTHER_PC, Callback: result =>
            {
                if(result.param2 == 1)
                    player.FinishQuest(Id);
            });
        }
        public override void OnGossip(ulong actorId)
        {
            if(actorId == Data.ACTOR0)
                Scene00000();
            else if (actorId == Data.ACTOR1)
                Scene00005();
            else if (actorId == Data.ACTOR2)
                Scene00011();
        }
        
    }
}