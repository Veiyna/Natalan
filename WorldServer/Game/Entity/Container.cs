using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shared.Database.Datacentre;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game.Entity;

public class Container
{
    public ContainerType ContainerType { get; }
    public ushort Count => (ushort)items.Count(i => i != null);
        
    private readonly List<Item> items;
    
    public void Save(DataCentreDatabase context)
    {
        foreach (var item in items)
        {
            item?.Save(context);
        }
    }
    public Container(ContainerType containerType, ushort capacity)
    {
        ContainerType = containerType;
        items = new List<Item>(new Item[capacity]);
    }

    /// <summary>
    /// Find all item instances and their current slot by item id.
    /// </summary>
    public IEnumerable<(ushort Slot, Item Item)> GetItems(uint itemId)
    {
        for (ushort i = 0; i < items.Count; i++)
            if (items[i]?.Entry.RowId == itemId)
                yield return (i, items[i]);
    }

    /// <summary>
    /// Find a single item instance by Guid.
    /// </summary>
    public Item GetItem(ulong guid)
    {
        return items.SingleOrDefault(i => i != null && i.Guid == guid);
    }

    /// <summary>
    /// Find a single item instance by container slot.
    /// </summary>
    public Item GetItem(ushort slot)
    {
        if (slot >= items.Count)
            throw new ArgumentException($"Invalid slot {slot}!");

        return items[slot];
    }

    public ushort GetFirstAvailableSlot()
    {
        for (ushort i = 0; i < items.Count; i++)
            if (items[i] == null || items[i].Entry.RowId == 0)
                return i;

        return ushort.MaxValue;
    }
        
    public void AddItem(Item item, ushort slot, bool update = false)
    {
        Debug.Assert(item != null);
        Debug.Assert(slot < items.Count);
        items[slot] = item;

#if DEBUG
        //Console.WriteLine($"Adding item {item.Entry.RowId} to container: {ContainerType}, slot: {slot}");
#endif
            
        item.UpdatePosition(ContainerType, slot, update);
    }

    public void RemoveItem(Item item)
    {
        Debug.Assert(item != null);
        items[item.Slot] = null;


#if DEBUG
        Console.WriteLine($"Removing item {item.Entry.RowId} from container: {ContainerType}, slot: {item.Slot}");
#endif
            
        item.UpdatePosition(ContainerType.None, ushort.MaxValue);
    }
        
    /// <summary>
    /// Send container item instance information to client, only sent on login.
    /// </summary>
    public void SendItemSetup(uint index)
    {
        items.ForEach(i => i?.SendSetup(index));
    }
}