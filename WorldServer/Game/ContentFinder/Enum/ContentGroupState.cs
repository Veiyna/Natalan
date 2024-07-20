namespace WorldServer.Game.ContentFinder.Enum
{
    public enum ContentGroupState : byte
    {
        None = 0,
        MatchingInProgress = 1,
        MatchingComplete = 2,
        WaitingForAccept = 3,
        Accepted = 4,
        InProgress = 5,
        InProgressRefill = 6,
        ToBeRemoved = 7
    };
}