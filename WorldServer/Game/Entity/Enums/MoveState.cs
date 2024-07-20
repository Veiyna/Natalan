namespace WorldServer.Game.Entity.Enums;

public enum MoveState : byte
{
    No = 0x00,
    LeaveCollision = 0x01,
    EnterCollision = 0x02,
    StartFalling = 0x04,
}