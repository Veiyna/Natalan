using Shared.Game;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ActorHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientNewWorld, SubPacketHandlerFlags.RequiresPlayer)]
        public static void HandleNewWorld(WorldSession session, SubPacket subPacket)
        {
            if (!session.Player.IsLogin || session.Player.InWorld)
                return;

            session.Player.OnLogin();
            MapManager.AddToMap(session.Player);
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientTerritoryFinalise, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientTerritoryFinalise(WorldSession session, SubPacket subPacket)
        {
            session.Player.SendVisible();
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientPlayerMove, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientPlayerMove(WorldSession session, ClientPlayerMove actorMove)
        {
            var newPosition = new WorldPosition((ushort)session.Player.Map.Entry.RowId, actorMove.Position, actorMove.Orientation);
            var clientAnimationType = actorMove.ClientAnimationType;
            var animationState = actorMove.AnimationState;
            var animationType = actorMove.AnimationType;
            var headPosition = actorMove.HeadPosition;
            var originalAnimationType = actorMove.AnimationType;
            var animationSpeed = MoveSpeed.Walk;
            var unknownRotation = 0;
            animationType |= actorMove.ClientAnimationType;

            if (animationType.HasFlag(MoveType.Strafing))
            {
                if (animationType.HasFlag(MoveType.Walking))
                    headPosition = 0xFF;
                else if (headPosition < 0x7F)
                    headPosition += 0x7F;
                else if (headPosition > 0x7F)
                    headPosition -= 0x7F;
            }

            if (animationType == MoveType.Running)
            {
                headPosition = 0x7F;
                animationSpeed = MoveSpeed.Run;
            }

            
            //TODO: This doesn't seem right.
            if (animationType.HasFlag(MoveType.Jumping))
            {
                if (animationState == MoveState.LeaveCollision)
                {
                    if ((originalAnimationType & clientAnimationType) != 0)
                        animationType += 0x10;
                    else
                        animationType += 0x04;
                }
                if (animationState == MoveState.StartFalling)
                    session.Player.IsFalling = true;
                if (animationState == MoveState.EnterCollision)
                {
                    animationType = MoveType.Walking;
                    session.Player.IsFalling = false;
                }

            }

            if (session.Player.IsFalling)
            {
                animationType += 0x10;
                unknownRotation = 0x7F;
            }

            session.Player.AnimationData = new AnimationData
            {
                AnimationType = animationType,
                AnimationSpeed = animationSpeed,
                AnimationState = animationState,
                HeadPosition = headPosition,
                UnknownRotation = (byte)unknownRotation,
            };
            
            
            session.Player.Relocate(newPosition);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientEquipDisplayFlags, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientEquipDisplayFlags(WorldSession session, ClientEquipDisplayFlags displayFlags)
        {
            session.Player.Character.EquipDisplayFlags = displayFlags.DisplayFlags;
            session.Player.SendMessageToVisible(new ServerEquipDisplayFlags()
            {
                DisplayFlags = displayFlags.DisplayFlags
            },true);
        }
    }
}
