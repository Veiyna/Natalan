using System;

namespace WorldServer.Game.Entity.Enums;

[Flags]
public enum ExamineDisplayFlags : byte
{
    Visor = 1,
    HideHead = 2,
    HideWeapon = 4
}