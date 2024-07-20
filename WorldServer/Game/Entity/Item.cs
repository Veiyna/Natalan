using System.Diagnostics;
using Shared.Database.Datacentre;
using Shared.Database.Datacentre.Models;
using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public class Item
{
    public Lumina.Excel.GeneratedSheets.Item Entry { get; }

    public ContainerType ContainerType { get; private set; } = ContainerType.None;
    public ushort Slot { get; private set; }
    public ulong Guid { get; }
    public uint StackSize { get; private set; }
        
    public uint Glamour { get; set; }
        
    public ushort Color { get; set; }
    public ushort Color2 { get; set; }

    public uint GetModel()
    {
            
        var model = (uint)Entry.ModelMain;
        if (Glamour != 0)
        {
            var modelMain = GameTableManager.Items.GetRow(this.Glamour)?.ModelMain;
            if (modelMain != null)
            {
                model = (uint)modelMain;
            }
        }

        model |= (uint)(this.Color << 24);

        return model;
    }

    private readonly Player owner;

    public Item(Player player, Lumina.Excel.GeneratedSheets.Item entry, ulong guid, uint stackSize = 1u)
    {
        Debug.Assert(player != null);
        Debug.Assert(entry != null);
            
        owner     = player;
        Entry     = entry;
        Guid      = guid;
        StackSize = stackSize;
    }
        
    public Item(ItemModel model, Player owner)
    {
        Entry = GameTableManager.Items.GetRow(model.ItemId);
        Guid = model.Id;
        this.owner = owner;
        ContainerType = (ContainerType)model.ContainerType;
        Slot = model.Slot;
        StackSize = model.StackSize;
        Color = model.Color;
        Color2 = model.Color2;
        Glamour = model.Glamour;

    }

    public void UpdatePosition(ContainerType containerType, ushort slot, bool update = false)
    {
        ContainerType = containerType;
        Slot          = slot;
            
        if (update)
            SendItemUpdate();
    }

    public void UpdateStackSize(uint stackChange, bool add = true)
    {
        Debug.Assert(stackChange != 0);
            
        checked
        {
            if (add)
                StackSize += stackChange;
            else
                StackSize -= stackChange;
        }
            
        SendItemUpdate();
    }

    public void UpdateColor(ushort? color, ushort? color2 = 0)
    {
        if (color != null)
        {
            this.Color = color.Value;
        }
            
        if (color2 != null)
        {
            this.Color2 = color2.Value;
        }
            
        SendItemUpdate();
    }
        
    public void UpdateGlamour(uint itemId)
    {
        Glamour = itemId;
        SendItemUpdate();
    }

    public void Save(DataCentreDatabase context)
    {
        this.owner.Character.Item.Add(new ItemModel
        {
            Id                 = this.Guid,
            ItemId = this.Entry.RowId,
            ContainerType = (ushort)this.ContainerType,
            Slot = this.Slot,
            StackSize = this.StackSize,
            Color = this.Color,
            Color2 = this.Color2,
            Glamour = this.Glamour
        });
    }
        
    public void SendSetup(uint index)
    {
        owner.Session.Send(new ServerItemSetup
        {
            Index         = index,
            ContainerType = ContainerType,
            Slot          = Slot,
            ItemId        = Entry.RowId,
            StackSize     = StackSize,
            Color         = Color,
            Color2 = Color2,
            Glamour = Glamour,
        });
    }
        
    private void SendItemUpdate()
    {
        if (!owner.InWorld)
            return;
            
        owner.Session.Send(new ServerItemUpdate
        {
            ContainerType = ContainerType,
            Slot          = Slot,
            ItemId        = Entry.RowId,
            StackSize     = StackSize,
            Color = Color,
            Color2 = Color2,
            Glam = Glamour
        });
    }
}