using System;

namespace WorldServer.Game.Entity.Enums;

[Flags]
public enum MoveType : byte
{
    Running = 0x00,
    Walking = 0x02,
    Strafing = 0x04,
    Jumping = 0x10,
}