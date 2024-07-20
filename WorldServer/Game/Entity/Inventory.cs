using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lumina.Excel.GeneratedSheets;
using Shared.Database.Datacentre;
using Shared.Database.Datacentre.Models;
using Shared.Game.Enum;
using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Manager;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public partial class Inventory
{
    private Player owner;
    private readonly Dictionary<ContainerType, Container> containers = new();
        
    public Inventory(Player player)
    {
        Initialise(owner);
    }

    /// <summary>
    /// initialise an inventory for a new character with starting gear for class and race
    /// </summary>
    public Inventory(Player owner, Shared.Game.Enum.Race raceId, Sex sexId, Shared.Game.Enum.ClassJob classJobId)
    {
        Initialise(owner);
            
        if (!GameTableManager.Races.TryGetValue((uint)raceId, out Lumina.Excel.GeneratedSheets.Race raceEntry))
            throw new ArgumentException($"Invalid race id {raceId}!");
                
        if (!GameTableManager.ClassJobs.TryGetValue((uint)classJobId, out Lumina.Excel.GeneratedSheets.ClassJob classJobEntry))
            throw new ArgumentException($"Invalid classJob id {classJobId}!");
            
        foreach (ItemModel itemModel in owner.Character.Item
                     .Select(i => i))
        {
            if (itemModel.ContainerType == 2005)
                continue;
            var item = new Item(itemModel, owner);
            AddItem(item, GetContainer((ContainerType)itemModel.ContainerType), itemModel.Slot);
        }
        if ((owner.Character.FlagsCu & PlayerFlagsCu.FirstLogin) != 0)
        {
            // default weapon and gear
            EquipItem(classJobEntry.ItemStartingWeapon.Row);
            if (sexId == Sex.Male)
            {
                EquipItem(raceEntry.RSEMBody.Row);
                EquipItem(raceEntry.RSEMFeet.Row);
                EquipItem(raceEntry.RSEMHands.Row);
                EquipItem(raceEntry.RSEMLegs.Row);
            }
            else
            {
                EquipItem(raceEntry.RSEFBody.Row);
                EquipItem(raceEntry.RSEFFeet.Row);
                EquipItem(raceEntry.RSEFHands.Row);
                EquipItem(raceEntry.RSEFLegs.Row);
            }

            // starting jewellery
            
            EquipItem(15130);
            EquipItem(15131);
            EquipItem(15132);
            EquipItem(15133);
        }

    }

    private void Initialise(Player player)
    {
        owner = player;
        foreach (ContainerType containerType in Enum.GetValues(typeof(ContainerType)))
        {
            if (containerType == ContainerType.None)
                continue;
                
            ushort capacity = AssetManager.ContainerCapacities[containerType];
            containers.Add(containerType, new Container(containerType, capacity));
        }
    }
        
    public void Save(DataCentreDatabase database)
    {
        this.owner.Character.Item.Clear();
        foreach (Container container in containers.Values)
            container.Save(database);
    }

    private Container GetContainer(ContainerType containerType)
    {
        if (containerType == ContainerType.None)
            return null;
            
        Debug.Assert(containers.ContainsKey(containerType));
        return containers[containerType];
    }
        
    /// <summary>
    /// Get visible equipped weapons (slots 0-1).
    /// </summary>
    public (ulong MainHand, ulong OffHand) GetWeaponDisplayIds()
    {
        Container container = GetContainer(ContainerType.Equipped);

        Item mainHand = container.GetItem((ushort)ContainerEquippedSlot.MainHand);
        Item offhand  = container.GetItem((ushort)ContainerEquippedSlot.OffHand);
            
        var mainHandEntry = mainHand?.Entry;
        var offHandEntry = offhand?.Entry;

        if (mainHand != null && mainHand.Glamour != 0 )
            mainHandEntry = GameTableManager.Items.GetRow(mainHand.Glamour);
            
        if (offhand != null && offhand.Glamour != 0 )
            offHandEntry = GameTableManager.Items.GetRow(offhand.Glamour);

        // some main hands have a secondary model (eg: bow and quiver)
        return (mainHandEntry?.ModelMain | (ulong)(mainHand?.Color ?? 0) << 48 ?? 0L,
            (mainHandEntry ?? offHandEntry)?.ModelSub | (ulong)(offhand?.Color ?? 0) << 48 ?? 0L);
    }
        
    public Item GetItemAtSlot(ContainerType type, ContainerEquippedSlot slot)
    {
        return GetContainer(type).GetItem((ushort)slot);
    }
        
    public Item GetItemAtSlot(ContainerType type, ushort slot)
    {
        return GetContainer(type).GetItem(slot);
    }

    /// <summary>
    /// Get visible equipped items excluding weapons (slots 2-4 and 6-12).
    /// </summary>
    public IEnumerable<uint> GetVisibleItemDisplayIds()
    {
        Container container = GetContainer(ContainerType.Equipped);
        for (ContainerEquippedSlot i = ContainerEquippedSlot.Head; i <= ContainerEquippedSlot.LeftRing; i++)
            if (i != ContainerEquippedSlot.Waist)
            {
                var item = container.GetItem((ushort)i);
                yield return item?.GetModel() ?? 0;
            }

    }
        
    public IEnumerable<ushort> GetSecondDyeIds()
    {
        Container container = GetContainer(ContainerType.Equipped);
        for (ContainerEquippedSlot i = ContainerEquippedSlot.Head; i <= ContainerEquippedSlot.LeftRing; i++)
            if (i != ContainerEquippedSlot.Waist)
            {
                var item = container.GetItem((ushort)i);
                yield return item?.Color2 ?? 0;
            }

    }

    private void AddItem(Item item, Container container, ushort slot, bool update = false)
    {
        Debug.Assert(item != null);
        Debug.Assert(container != null);

        if (container.ContainerType == ContainerType.Equipped)
            EquipItem(item, slot);
        else
            container.AddItem(item, slot, update);
    }

    public void NewItem(ContainerType containerType, uint itemId, ushort slot, uint count = 1u)
    {
        if (!GameTableManager.Items.TryGetValue(itemId, out Lumina.Excel.GeneratedSheets.Item itemEntry))
            throw new ArgumentException($"Invalid item id {itemId}!");
        Container container = GetContainer(containerType);
        AddItem(new Item(owner, itemEntry, AssetManager.NextItemId, count), container, slot, true);
    }

    private void RemoveItem(Item item, Container container)
    {
        Debug.Assert(item != null);
        Debug.Assert(container != null);

        if (container.ContainerType == ContainerType.Equipped)
            UnEquipItem(item);
        else
            container.RemoveItem(item);
    }

    public Item FindItem(uint itemId)
    {
        foreach (var container in this.containers.Values)
        {
            var item = container.GetItems(itemId).FirstOrDefault();
            if(item.Item != null)
                return item.Item;
        }

        return null;
    }
        
    public bool RemoveItem(uint itemId, uint amount = 1)
    {
        var item = FindItem(itemId);
        if (item == null) return false;
        var newStackSize = (int)(item.StackSize - amount);
        if (item.StackSize < amount) return false;
        if (newStackSize <= 0)
        {
            DiscardItem(item.ContainerType, item.Slot);
        }
        else
        {
            item.UpdateStackSize(amount, false);
        }

        return true;


    }
        
    /// <summary>
    /// Create a new item instance in the first valid inventory slot or stack.
    /// </summary>
    public void NewItem(uint itemId, uint count = 1u, bool sendLootMessage = true)
    {
        if(itemId == 0)
            return;
        if (!GameTableManager.Items.TryGetValue(itemId, out Lumina.Excel.GeneratedSheets.Item itemEntry))
            throw new ArgumentException($"Invalid item id {itemId}!");

        if (itemEntry.ItemUICategory.Row == 100)
            return;
                
        if (string.IsNullOrEmpty(itemEntry.Name.RawString))
            return;

        uint countLeft = count;
        for (ContainerType containerType = ContainerType.Inventory0; containerType <= ContainerType.Inventory3; containerType++)
        {
            if (countLeft == 0)
                break;

            Container container = GetContainer(containerType);
            if (itemEntry.StackSize > 1)
            {
                // update current item stacks before creating any new ones
                using (IEnumerator<(ushort Slot, Item Item)> enumerator = container.GetItems((uint)itemEntry.RowId).GetEnumerator())
                {
                    while (countLeft > 0 && enumerator.MoveNext())
                    {
                        uint stackChange = Math.Min((uint)itemEntry.StackSize - enumerator.Current.Item.StackSize, countLeft);
                        if (stackChange == 0)
                            continue;
                            
                        enumerator.Current.Item.UpdateStackSize(stackChange);
                        countLeft -= stackChange;
                    }
                }
            }
                
            // create new items or stacks for remaining count
            for (ushort slot = container.GetFirstAvailableSlot(); countLeft > 0 && slot != ushort.MaxValue; slot = container.GetFirstAvailableSlot())
            {
                uint stackSize = Math.Min((uint)itemEntry.StackSize, countLeft);
                AddItem(new Item(owner, itemEntry, AssetManager.NextItemId, stackSize), container, slot, true);
                    
                countLeft -= stackSize;
            }
        }
            
        if (owner.InWorld && sendLootMessage)
        {
            // shows new item message in chat
            /*
            owner.Session.Send(new ServerActorActionSelf
            {
                Action     = ActorActionServer.ItemCreate,
                Parameter1 = itemId,
                Parameter2 = Math.Min(count, count - countLeft)
            });*/
            this.owner.Session.Send(new ServerLootMessage
            {
                MessageType = LootMessageType.GetItem2,
                Var1 = this.owner.Character.ActorId,
                Var2 = itemId,
                Var3 = count
            });
        }
            
        // TODO: should something be done if there are no slots? Mail item ect?
    }

    /// <summary>
    /// Move an existing item instance to another free container slot.
    /// </summary>
    public void MoveItem(ContainerType source, ushort srcSlot, ContainerType destination, ushort dstSlot)
    {
        Container srcContainer = GetContainer(source);
        Container dstContainer = GetContainer(destination);

        Item srcItem = srcContainer.GetItem(srcSlot);
        if (srcItem == null)
            throw new ArgumentException($"Invalid source item in container: {source}, slot: {srcSlot}");

        if (dstContainer.GetItem(dstSlot) != null)
            throw new ArgumentException($"Can't move item to occupied container: {destination}, slot {dstSlot}!");

        try
        {
            RemoveItem(srcItem, srcContainer);
            AddItem(srcItem, dstContainer, dstSlot);
        }
        catch
        {
            // make sure item is returned to its original positions
            RollbackItem(srcItem, srcContainer, srcSlot);
            throw;
        }
    }
        
    public void DyeItem(uint itemContainer, ushort itemSlot, uint dyeContainer, ushort dyeSlot, uint dyeContainer2 = 9999, ushort dyeSlot2 = 65535)
    {
        Item item = GetContainer((ContainerType)itemContainer).GetItem(itemSlot);
            
        ushort? color1 = null;
        ushort? color2 = null;
            
        if (dyeContainer != 9999 && dyeSlot != 65535)
        {
            color1 = (ushort?)GetContainer((ContainerType)dyeContainer).GetItem(dyeSlot).Entry.AdditionalData;
        }

        if (dyeContainer2 != 9999 && dyeSlot2 != 65535)
        {
            color2 = (ushort?)GetContainer((ContainerType)dyeContainer2).GetItem(dyeSlot2).Entry.AdditionalData;
        }

        this.owner.SendStateFlags();
            
        if (item == null || (color1 == null && color2 == null)) return;
                
        item.UpdateColor(color1, color2);
            
        if(item.ContainerType == ContainerType.Equipped) 
            SendActorAppearanceUpdate((ContainerEquippedSlot)item.Slot);
            
            
    }
        
    public void GlamourItem(uint itemContainer, ushort itemSlot, uint dyeContainer, ushort dyeSlot)
    {
        Item item = GetContainer((ContainerType)itemContainer).GetItem(itemSlot);
        Item glamItem = GetContainer((ContainerType)dyeContainer).GetItem(dyeSlot);
            
        if (item == null || glamItem == null) return;

        item.Color = glamItem.Color;
        item.UpdateGlamour(glamItem.Entry.RowId);
            
        if(item.ContainerType == ContainerType.Equipped) 
            SendActorAppearanceUpdate((ContainerEquippedSlot)item.Slot);
            
            
    }
        
    public void GlamourItem(uint itemContainer, ushort itemSlot, GlamourDresserEntry entry)
    {
        Item item = GetContainer((ContainerType)itemContainer).GetItem(itemSlot);

        this.owner.SendStateFlags();
            
        if (item == null) return;

        item.Color = entry.Color;
        item.UpdateGlamour(entry.ItemId);
            
            
        if(item.ContainerType == ContainerType.Equipped) 
            SendActorAppearanceUpdate((ContainerEquippedSlot)item.Slot);
            
            
    }
        
    public void RemoveGlamour(uint itemContainer, ushort itemSlot)
    {
        Item item = GetContainer((ContainerType)itemContainer).GetItem(itemSlot);
            
            
        if (item == null ) return;

        item.UpdateGlamour(0);

        if (item.ContainerType == ContainerType.Equipped)
            SendActorAppearanceUpdate((ContainerEquippedSlot)item.Slot);


    }

    private void RollbackItem(Item item, Container originalContainer, ushort originalSlot)
    {
        Debug.Assert(item != null);

        if (item.ContainerType != originalContainer.ContainerType)
        {
            RemoveItem(item, GetContainer(item.ContainerType));
            AddItem(item, originalContainer, originalSlot);
        }

        if (item.Slot != originalSlot)
            item.UpdatePosition(item.ContainerType, originalSlot);
    }

    /// <summary>
    /// Equip a new item instance into it's first valid slot.
    /// </summary>
    public void EquipItem(uint itemId)
    {
        if (!GameTableManager.Items.TryGetValue(itemId, out Lumina.Excel.GeneratedSheets.Item itemEntry))
            throw new ArgumentException($"Invalid item id {itemId}!");

        EquipSlotCategory equipSlotCategoryEntry = itemEntry.EquipSlotCategory.Value;
        if (equipSlotCategoryEntry == null)
            throw new ArgumentException($"Item id {itemId} can't be equipped!");

        // find first free slot for the item (some items such as rings can be equipped into multiple slots)
        Container container = GetContainer(ContainerType.Equipped);
            
        foreach (var equipSlot in CheckPossibleSlots(equipSlotCategoryEntry))
        {
            if (container.GetItem((ushort)equipSlot) != null)
                continue;

            _EquipItem(new Item(owner, itemEntry, AssetManager.NextItemId), (ushort)equipSlot);
            break;
        }
            
    }
        
    private List<sbyte> CheckPossibleSlots(EquipSlotCategory equipSlotCategory) {
        var possible = new List<sbyte>
        {
            equipSlotCategory.MainHand,
            equipSlotCategory.OffHand,
            equipSlotCategory.Head,
            equipSlotCategory.Body,
            equipSlotCategory.Gloves,
            equipSlotCategory.Waist,
            equipSlotCategory.Legs,
            equipSlotCategory.Feet,
            equipSlotCategory.Ears,
            equipSlotCategory.Neck,
            equipSlotCategory.Wrists,
            equipSlotCategory.FingerL,
            equipSlotCategory.FingerR,
            equipSlotCategory.SoulCrystal
        };

        var final = new List<sbyte>();
        var i = 0;
        foreach (var slot in possible)
        {
            if (slot == 1)
            {
                final.Add((sbyte)i);
                    
            }

            i++;
        }
        return final;
    }
        
    private void EquipItem(Item item, ushort slot)
    {
        EquipSlotCategory equipSlotCategoryEntry = item.Entry.EquipSlotCategory.Value;
        if (equipSlotCategoryEntry == null)
            throw new ArgumentException($"Item id {item.Entry.RowId} can't be equipped!");
        /*
        if (equipSlotCategoryEntry.PossibleSlots.All(s => s.Key != slot))
            throw new ArgumentException($"Item id {item.Entry.RowId} can't be equipped into slot {slot}!");
        */
        _EquipItem(item, slot);
    }

    private void _EquipItem(Item item, ushort slot)
    {
        Debug.Assert(item != null);
            
        GetContainer(ContainerType.Equipped).AddItem(item, slot);
        SendActorAppearanceUpdate((ContainerEquippedSlot)slot);
        if (slot is 0 or 13)
        {
                
            var classJobId = (byte)item.Entry.ClassJobUse.Value.RowId;
            /*
            var classEntry = GameTableManager.ClassJobs.GetRow(classJobId);
            var soulCrystalItemEntry = classEntry.ItemSoulCrystal.Value;

            if (soulCrystalItemEntry != null)
            {
                if (slot == 0)
                {
                    var soulCrystal = GetItemAtSlot(ContainerType.Equipped, 13);
                    if (soulCrystal == null)
                    {
                        var foundSoulCrystal = FindItem(soulCrystalItemEntry.RowId);
                        MoveItem(foundSoulCrystal.ContainerType, foundSoulCrystal.Slot, ContainerType.Equipped, 13);

                    }
                }
            }
            */
            /*
            var jobcrystalid = item.Entry.ClassJobUse.Value.ItemSoulCrystal.Value.RowId;
            var soulcrystal = GetItemAtSlot(ContainerType.Equipped, ContainerEquippedSlot.SoulCrystal);
            */
            if(this.owner.Character.ClassJobId != classJobId)
                this.owner.ChangeClass(classJobId);
                    
        }
    }

    private void UnEquipItem(Item item)
    {
        Debug.Assert(item != null);
            
        GetContainer(ContainerType.Equipped).RemoveItem(item);
        SendActorAppearanceUpdate((ContainerEquippedSlot)item.Slot);
    }

    private void SendActorAppearanceUpdate(ContainerEquippedSlot slot)
    {
        // waist items don't update visual appearance
        if (!owner.InWorld || slot == ContainerEquippedSlot.Waist)
            return;
            
        (ulong MainHand, ulong OffHand) weaponDisplayId = owner.Inventory.GetWeaponDisplayIds();
        owner.SendMessageToVisible(new ServerActorAppearanceUpdate
        {
            Character = owner.Character,
            MainHandDisplayId     = weaponDisplayId.MainHand,
            OffHandDisplayId      = weaponDisplayId.OffHand,
            VisibleItemDisplayIds = GetVisibleItemDisplayIds(),
            VisibleSecondDyeIds = GetSecondDyeIds()
        }, true);
    }
        
    /// <summary>
    /// Swaps two existing item instances and their positions with each other.
    /// </summary>
    public void SwapItems(ContainerType source, ushort srcSlot, ContainerType destination, ushort dstSlot)
    {
        Container srcContainer = GetContainer(source);
        Container dstContainer = GetContainer(destination);

        Item srcItem = srcContainer.GetItem(srcSlot);
        if (srcItem == null)
            throw new ArgumentException($"Invalid source item in container: {source}, slot: {srcSlot}");

        Item dstItem = dstContainer.GetItem(dstSlot);
        if (dstItem == null)
            throw new ArgumentException($"Invalid destination item in container: {destination}, slot: {dstSlot}");

        try
        {
            RemoveItem(srcItem, srcContainer);
            RemoveItem(dstItem, dstContainer);
            AddItem(srcItem, dstContainer, dstSlot);

            // swapping an equipped item moves it to the appropriate Armoury Chest rather than the source location if not Armoury already
            if (destination == ContainerType.Equipped && (source < ContainerType.ArmouryOffHand || source > ContainerType.ArmouryMainHand))
            {
                srcContainer = GetContainer(AssetManager.EquipArmouryContainerTypes[(ItemUiCategory)dstItem.Entry.ItemUICategory.Row]);
                srcSlot      = srcContainer.GetFirstAvailableSlot();

                // TODO: some error about full armoury
                if (srcSlot == ushort.MaxValue)
                    return;
            }
                
            AddItem(dstItem, srcContainer, srcSlot);
        }
        catch
        {
            // make sure items are returned to their original positions
            RollbackItem(srcItem, srcContainer, srcSlot);
            RollbackItem(dstItem, dstContainer, dstSlot);
            throw;
        }
    }

    /// <summary>
    /// Destroy an existing item instance permanently.
    /// </summary>
    public void DiscardItem(ContainerType source, ushort srcSlot)
    {
        Item srcItem = GetContainer(source).GetItem(srcSlot);
        if (srcItem == null)
            throw new ArgumentException($"Invalid source item in container: {source}, slot: {srcSlot}");

        var transaction = new InventoryTransaction(owner);
        transaction.Add(InventoryAction.Discard, source, srcSlot);
        transaction.Commit();
    }
        
    // TODO: split/stack

    public void Send()
    {
        // TODO: some sequential value, items don't show without it
        uint index = 0u;
        foreach (KeyValuePair<ContainerType, Container> pair in containers)
        {
            pair.Value.SendItemSetup(index);
            owner.Session.Send(new ServerContainerSetup
            {
                Index     = index++,
                Type      = pair.Key,
                ItemCount = pair.Value.Count
            });
        }
    }
}