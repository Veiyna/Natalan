namespace WorldServer.Game.Entity.Enums;

public enum EquipDisplayFlags : byte
{
    HideNothing = 0x0,
    HideHead = 0x1,
    HideWeapon = 0x2,
    HideLegacyMark = 0x4,

    StoreNewItemsInArmouryChest = 0x10,
    StoreCraftedItemsInInventory = 0x20,

    Visor = 0x40,
};