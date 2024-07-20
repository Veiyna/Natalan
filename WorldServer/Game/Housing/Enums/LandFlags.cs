using System;

namespace WorldServer.Game.Housing.Enums;

[Flags]
public enum LandFlags : uint
{
    EstateBuilt = 0x1,
    HasAetheryte = 0x2,
    UNKNOWN_1 = 0x4,
    UNKNOWN_2 = 0x8,
    UNKNOWN_3 = 0x10,
};