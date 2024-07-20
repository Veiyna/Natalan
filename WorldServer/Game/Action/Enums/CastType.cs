namespace WorldServer.Game.Action.Enums
{
    public enum CastType : byte
    {
        SingleTarget = 1,
        CircularAOE = 2,
        Type3 = 3, // another single target? no idea how to call it
        RectangularAOE = 4,
        CircularAoEPlaced = 7
    };
}