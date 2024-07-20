using Shared.Game;
using Shared.Game.Enum;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Opening
{
    [EventScript(0x130003)]
    public class OpeningUldah : EventScript
    {
        private static class Data
        {
            public const byte SceneOpening       = 0;
            public const byte SceneControlScheme = 1;
            public const byte SceneOutOfBounds   = 20;
            public const byte SceneLogin         = 40;
            public const uint NpcRyssfloh        = 0x3E9699;
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

        public override void OnOutOfBounds(ulong actorId, WorldPosition position)
        {
            owner.Event.NewScene(Id,Data.SceneOutOfBounds, (SceneFlags)0x2001, null, 1, Data.NpcRyssfloh, 9);
        }

        public override void OnEventTerritory()
        {
            if (this.owner.Character.OpeningSequence == 0)
                owner.Event.NewScene(this.Id,Data.SceneOpening, (SceneFlags)0x4BAC05);
            else
                owner.Event.NewScene(this.Id,Data.SceneLogin, SceneFlags.NO_DEFAULT_CAMERA);
        }

        public override void OnSceneFinish(ushort sceneId, SceneResult result)
        {
            switch (sceneId)
            {
                case Data.SceneOpening:
                    this.owner.Character.OpeningSequence = 1;
                    owner.Event.NewScene(this.Id,Data.SceneControlScheme, (SceneFlags)0x2001, null, 0, 2u, 0x2000u);
                    break;
                case Data.SceneControlScheme:
                case Data.SceneOutOfBounds:
                case Data.SceneLogin:
                    owner.Event.StopEvent(this.Id);
                    break;
            }
        }
    }
}
