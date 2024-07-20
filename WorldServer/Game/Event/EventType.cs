namespace WorldServer.Game.Event
{
    public enum EventType : byte
    {
        Gossip      = 1,
        Emote       = 2,
        Nest        = 7,
        WithinRange = 10,
        OutOfBounds = 11,
        Territory   = 15,
        ActionResult = 18,
    }
}
