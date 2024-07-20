namespace Shared.Game.Enum;

public enum LootMessageType : byte
{
    GetItem1 = 1, // p1: actorId, p4: itemId (HQ: itemId + 1,000,000 lol), p5: amount
    GetItem2 = 3, // p1: actorId, p2: itemId, p3: amount, seems like same thing as GetItem1 but different param position.
    FailedToGetLootNoFreeInventorySlot = 5, // p1: actorId
    LootRolled = 7, // p1: actorId, p2: itemId, p3: amount
    GetGil = 9, // p1: gil
    EmptyCoffer = 11, // seems like no param
};