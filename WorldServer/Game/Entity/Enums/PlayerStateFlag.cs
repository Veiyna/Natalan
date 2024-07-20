namespace WorldServer.Game.Entity.Enums;

public enum PlayerStateFlag : byte
{
    HideUILockChar = 0, // as the name suggests, hides the ui and logs the char...
    InCombat = 1, // in Combat, locks gearchange/return/teleport
    Casting = 2,
    Stuck = 3,
    InNpcEvent = 7, // when talking to a npc, locks ui giving "occupied" message

    InNpcEvent1 = 10, // Sent together with InNpcEvent, when waiting for input? just a guess...
    InDuelingArea = 11, //Sent after entering a dueling area

    BetweenAreas = 24,
    BoundByDuty = 28,
    OnFreeTrial = 40,
    Performing = 41,
    SomeWeirdFlag = 47,
    WatchingCutscene = 50, // this is actually just a dummy, this id is different


};