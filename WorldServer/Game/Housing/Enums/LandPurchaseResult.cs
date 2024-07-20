namespace WorldServer.Game.Housing.Enums;

public enum LandPurchaseResult : byte
{
    SUCCESS,
    ERR_NOT_ENOUGH_GIL,
    ERR_NOT_AVAILABLE,
    ERR_NO_MORE_LANDS_FOR_CHAR,
    ERR_INTERNAL
}