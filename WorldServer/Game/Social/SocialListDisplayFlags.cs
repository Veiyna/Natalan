using System;

namespace WorldServer.Game.Social
{
    [Flags]
    public enum SocialListDisplayFlags : byte
    {
        None     = 0,
        Unknown = 1 << 0,
        NoviceNetworkJoined  = 1 << 1,
        AnotherWorld   = 1 << 2,
        Wanderer   = 1 << 3,
        Traveler   = 1 << 4
    }
}