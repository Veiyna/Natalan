using System.Numerics;
using Shared.Command;
using Shared.Game;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Command
{
    public static class ActorHandler
    {
        // actor_action typeId
        [CommandHandler("actor_action", SecurityLevel.Developer, 7)]
        public static void HandleActorControl(WorldSession session, params string[] parameters)
        {
            if (!uint.TryParse(parameters[0], out var type))
                return;
            if (!uint.TryParse(parameters[1], out var param1))
                return;
            if (!uint.TryParse(parameters[2], out var param2))
                return;
            if (!uint.TryParse(parameters[3], out var param3))
                return;
            if (!uint.TryParse(parameters[4], out var param4))
                return;
            if (!uint.TryParse(parameters[5], out var param5))
                return;
            if (!uint.TryParse(parameters[6], out var param6))
                return;


            session.Send(new ServerActorActionSelf
            {
                Action = (ActorActionServer)type,
                Parameter1 = param1,
                Parameter2 = param2,
                Parameter3 = param3,
                Parameter4 = param4,
                Parameter5 = param5,
                Parameter6 = param6
            });
        }

        // actor_teleport x y z territoryId
        [CommandHandler("actor_teleport", SecurityLevel.Developer, 4)]
        public static void HandleActorTeleport(WorldSession session, params string[] parameters)
        {
            var offset = new float[3];
            for (var i = 0; i < 3; i++)
                if (!float.TryParse(parameters[i], out offset[i]))
                    return;

            if (!ushort.TryParse(parameters[3], out var territoryId))
                return;

            session.Player.TeleportTo(new WorldPosition(territoryId, new Vector3(offset[0], offset[1], offset[2]), 0f));
        }
    }
}