using Shared.Game;
using Shared.Game.Enum;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Opening
{
    [EventScript(0x130002)]
    public class OpeningGridania : EventScript
    {
        private static class Data
        {
            public const byte SceneOpening       = 0;
            public const byte SceneControlScheme = 1;
            public const byte SceneOutOfBounds   = 20;
            public const byte SceneLogin         = 40;
            public const uint NpcRyssfloh        = 2;
            public const uint ERANGE_SEQ_1_CLOSED_1 = 2351918;
            public const uint ERANGE_SEQ_1_CLOSED_2 = 2351919;
        }

        private void Scene00000()
        {
            owner.Event.NewScene(this.Id,Data.SceneOpening, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.INVIS_ENPC |
                                                            SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI |
                                                            SceneFlags.HIDE_HOTBAR | SceneFlags.SILENT_ENTER_TERRI_ENV | SceneFlags.SILENT_ENTER_TERRI_BGM | SceneFlags.SILENT_ENTER_TERRI_SE |
                                                            SceneFlags.DISABLE_SKIP | SceneFlags.DISABLE_STEALTH, _ =>
            {
                this.owner.Character.OpeningSequence = 1;
                Scene00001();
            });

        }

        private void Scene00001()
        {
            owner.Event.NewScene(this.Id,Data.SceneControlScheme, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.HIDE_HOTBAR);
        }

        private void Scene00010()
        {
            owner.Event.NewScene(this.Id,10, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.HIDE_HOTBAR);
        }
        private void Scene00020()
        {
            owner.Event.NewScene(this.Id,Data.SceneOutOfBounds, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.HIDE_HOTBAR);
        }
        
        private void Scene00030()
        {
            owner.Event.NewScene(this.Id,30, SceneFlags.NO_DEFAULT_CAMERA | SceneFlags.HIDE_HOTBAR);
        }
        private void Scene00040()
        {
            owner.Event.NewScene(this.Id,Data.SceneLogin, SceneFlags.NO_DEFAULT_CAMERA, _ =>
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
                    return 0xB7;
                case EventType.OutOfBounds:
                    return Data.NpcRyssfloh;
                default:
                    return 0u;
            }
        }

        public override void OnAreaTrigger(ulong actorId, WorldPosition position)
        {
            if(actorId is Data.ERANGE_SEQ_1_CLOSED_1 or Data.ERANGE_SEQ_1_CLOSED_2)
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
