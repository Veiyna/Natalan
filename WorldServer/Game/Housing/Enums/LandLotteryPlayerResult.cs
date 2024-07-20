namespace WorldServer.Game.Housing.Enums;

public enum LandLotteryPlayerResult : byte
{
    NoEntry,
    Entered,
    Winner,
    WinnerForfeit,
    Loser,
    RefundExpired
}