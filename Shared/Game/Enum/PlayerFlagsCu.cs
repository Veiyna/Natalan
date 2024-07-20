using System;

namespace Shared.Game.Enum
{
    [Flags]
    public enum PlayerFlagsCu : ushort
    {
        None       = 0x0000,
        FirstLogin = 0x0001,
        Invisible  = 0x0002,
        ImmediatelyAction = 0x0003,
    }
}
