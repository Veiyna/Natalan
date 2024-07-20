namespace WorldServer.Game.Event.Director.Enum;

public enum DirectorEventId : uint
{
    DEBUG_TimeSync = 0xC0000001,
    DutyCommence = 0x40000001,
    DutyComplete = 0x40000002,
    SetStringendoMode = 0x40000003,
    SetDutyTime = 0x40000004,
    LoadingScreen = 0x40000005,
    Forward = 0x40000006,
    BattleGroundMusic = 0x40000007,
    InvalidateTodoList = 0x40000008,
    VoteState = 0x40000009,
    VoteStart = 0x4000000A,
    VoteResult = 0x4000000B,
    VoteFinish = 0x4000000C,
    FirstTimeNotify = 0x4000000D,
    TreasureVoteRefresh = 0x4000000E,
    SetSharedGroupId = 0x4000000F,
};