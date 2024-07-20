using Shared.Game;
using Shared.Game.Enum;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Opening
{
    [EventScript(0x130001)]
    public class OpeningLimsaLominsa : EventScript
    {
        private static class Data
        {
            public const byte SceneOpening       = 0;
            public const byte SceneControlScheme = 1;
            public const byte SceneOutOfBounds   = 20;
            public const byte SceneLogin         = 40;
            public const uint NpcRyssfloh        = 0x3E9699;
            public const uint ERANGE_HOWTO_ANN_AND_QUEST = 4101831;
            public const uint ERANGE_HOWTO_QUEST_REWARD = 4102066;
            public const uint ERANGE_SEQ_1_CLOSED_1 = 4101785;
            public const uint POS_SEQ_1_CLOSED_RETURN_1 = 4101797;
            public const uint ERANGE_ALWAYS_CLOSED_1 = 4101744;
            public const uint POS_ALWAYS_CLOSED_RETURN_1 = 4101761;
            public const uint ENPC_ALWAYS_CLOSED_1 = 4102038;
            public const uint ERANGE_ALWAYS_CLOSED_2 = 4101746;
            public const uint POS_ALWAYS_CLOSED_RETURN_2 = 4101763;
            public const uint ENPC_ALWAYS_CLOSED_2 = 4102036;
            public const uint ERANGE_ALWAYS_CLOSED_3 = 4101967;
            public const uint POS_ALWAYS_CLOSED_RETURN_3 = 4101982;
            public const uint ENPC_ALWAYS_CLOSED_3 = 4102033;
            public const uint ERANGE_ALWAYS_CLOSED_4 = 4101970;
            public const uint POS_ALWAYS_CLOSED_RETURN_4 = 4101984;
            public const uint ENPC_ALWAYS_CLOSED_4 = 4102031;
            public const uint ERANGE_ALWAYS_CLOSED_5 = 4101973;
            public const uint POS_ALWAYS_CLOSED_RETURN_5 = 4101985;
            public const uint ENPC_ALWAYS_CLOSED_5 = 4102007;
            public const uint ERANGE_ALWAYS_CLOSED_6 = 4101979;
            public const uint POS_ALWAYS_CLOSED_RETURN_6 = 4101988;
            public const uint ENPC_ALWAYS_CLOSED_6 = 2367400;
            public const uint BGM_MUSIC_ZONE_SEA_TWN = 1020;
            public const uint NCUT_SEA_1 = 200;
            public const uint NCUT_SEA_2 = 132;
            public const uint NCUT_SEA_3 = 201;
            public const uint ENPC_QUEST_OFFER = 4102039;
            public const uint NCUT_LIGHT_ALL = 2;
            public const uint NCUT_LIGHT_SEA_1 = 147;
            public const uint NCUT_LIGHT_SEA_2 = 138;

        }

        private void Scene00000()
        {
            owner.Event.NewScene(this.Id,Data.SceneOpening, (SceneFlags)0x4BAC05, _ =>
            {
                this.owner.Character.OpeningSequence = 1;
                Scene00001();
            }, 1);

        }

        private void Scene00001()
        {
            owner.Event.NewScene(this.Id,Data.SceneControlScheme, (SceneFlags)0x2001, null, 0, 2u, 0x2000u);
        }

        private void Scene00010()
        {
            owner.Event.NewScene(this.Id,10, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.HIDE_HOTBAR);
        }
        private void Scene00020()
        {
            owner.Event.NewScene(this.Id,Data.SceneOutOfBounds, (SceneFlags)0x2001, null, Data.NpcRyssfloh, 9);
        }
        
        private void Scene00030()
        {
            owner.Event.NewScene(this.Id,30, (SceneFlags)0x2001,  null,1, Data.NpcRyssfloh, 9);
        }
        private void Scene00040()
        {
            owner.Event.NewScene(this.Id,Data.SceneLogin, SceneFlags.NO_DEFAULT_CAMERA,Callback: _ =>
            {
                if(this.owner.Character.OpeningSequence == 2)
                    Scene00030();
            });
        }
        public override uint GetParameter(EventType type)
        {
            switch (type)
            {
                case EventType.Territory:
                    return 0xB5;
                case EventType.OutOfBounds:
                    return Data.NpcRyssfloh;
                default:
                    return 0u;
            }
        }

        public override void OnAreaTrigger(ulong actorId, WorldPosition position)
        {
            if(actorId is Data.ERANGE_ALWAYS_CLOSED_1 or Data.ERANGE_ALWAYS_CLOSED_2 or Data.ERANGE_ALWAYS_CLOSED_3 or Data.ERANGE_ALWAYS_CLOSED_4 or Data.ERANGE_ALWAYS_CLOSED_5 or Data.ERANGE_ALWAYS_CLOSED_6)
                Scene00010();
        }

        public override void OnOutOfBounds(ulong actorId, WorldPosition position)
        {
            if(actorId == Data.ERANGE_SEQ_1_CLOSED_1)
                Scene00020();
        }

        public override void OnEventTerritory()
        {
            if (this.owner.Character.OpeningSequence == 0)
                Scene00000();
            else
                Scene00040();
                
        }


    }
}
