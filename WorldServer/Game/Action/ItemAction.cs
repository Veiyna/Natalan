using System;
using System.Linq;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Action.Enums;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;
using Item = Lumina.Excel.GeneratedSheets.Item;

namespace WorldServer.Game.Action;

public class ItemAction : Action
{
    private Lumina.Excel.GeneratedSheets.ItemAction ItemActionEntry;
    private Item ItemEntry;
    private ContainerType ItemContainer;
    private ushort ItemSlot;
    public ItemAction(Character caster, uint actionId, ushort sequence, ulong targetId, WorldPosition sourcePosition, SkillType skillType, uint additionalId, Lumina.Excel.GeneratedSheets.ItemAction itemAction, ushort itemContainer, ushort itemSlot, Item itemEntry) : base(caster, actionId, sequence, targetId, sourcePosition, skillType, additionalId)
    {
        this.ItemActionEntry = itemAction;
        this.ItemContainer = (ContainerType)itemContainer;
        this.ItemSlot = itemSlot;
        this.ItemEntry = itemEntry;
        this.CastTimeMs = (uint)(itemEntry.CastTimes * 1000);
    }

    public override void Execute()
    {
        base.Execute();

        switch ((ItemActionType)this.ItemActionEntry.Type)
        {
            case ItemActionType.ItemActionHeal:
            {
                HandleHeal();
                break;
            }
            case ItemActionType.ItemActionCoffer:
            {
                HandleCoffer();
                break;
            }
            case ItemActionType.ItemActionMount:
            {
                HandleMountUnlock();
                break;
            }
            case ItemActionType.ItemActionUnlock:
            {
                HandleActionUnlock();
                break;
            }
            case ItemActionType.ItemActionFramer:
            {
                HandleFramerUnlock();
                break;
            }
            case ItemActionType.ItemActionGlasses:
            {
                HandleGlassesUnlock();
                break;
            }
            case ItemActionType.ItemActionFantasia:
            {
                HandleFantasia();
                break;
            }
            default:
            {
                this.Source.ToPlayer.sendUrgent($"Not implemented. Type: {this.ItemActionEntry.Type}");
                break;
            }
        }
    }

    private void HandleMountUnlock()
    {
        var player = this.Source.ToPlayer;
        player.SetMountUnlock(this.ItemActionEntry.Data[0]);
        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
        
    }

    private void HandleHeal()
    {
        var player = this.Source.ToPlayer;
        player.Heal(this.ItemActionEntry.Data[1]); // TODO: Handle % heal
    }
    
    private void HandleFantasia()
    {
        var player = this.Source.ToPlayer;
        player.Character.RemakeFlags = 0x4;
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.FantasiaMsg
        });
        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
    }
    private void HandleCoffer()
    {
        var player = this.Source.ToPlayer;
        DataManager.Coffers.TryGetValue(this.ItemEntry.RowId, out var coffer);
        if (coffer == null || coffer.Items.Count == 0)
        {
            player.sendUrgent("Coffer data not found");
            return;
        }

        var items = coffer.Items;

        switch (coffer.Mode)
        {
            case CofferItemMode.All:
            {
                foreach (var itemId in items)
                {
                    player.Inventory.NewItem(itemId);
                }

                break;
            }
            case CofferItemMode.Random:
            {
                var index = Random.Shared.Next(items.Count);
                player.Inventory.NewItem(items[index]);
                break;
            }
            case CofferItemMode.ClassDependant:
            {
                // TODO: Use ClassJobCategory instead of ClassJobUse? What does retail do if you open a coffer as a class that doesn't have any items inside?
                var itemEntries = items.Select(itemId => GameTableManager.Items.GetRow(itemId));
                var itemsForClass = itemEntries.Where(i => i?.ClassJobUse.Row == player.Character.ClassId).Select(i => i.RowId).ToList();
                if (itemsForClass.Count == 0)
                {
                    player.sendUrgent("No items for this class");
                    return;
                }
                foreach (var itemId in itemsForClass)
                {
                    player.Inventory.NewItem(itemId);
                }
                break;
            }
                
        }


        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
        
    }

    private void HandleFramerUnlock()
    {
        var player = this.Source.ToPlayer;
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.FramerKitUnlock,
            Parameter1 = 80,
            Parameter2 = 1,
            Parameter3 = this.ItemEntry.RowId
        });
        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
    }
    
    private void HandleGlassesUnlock()
    {
        var player = this.Source.ToPlayer;
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.GlassesUnlock,
            Parameter1 = 0, // TODO: Not sure how to get the ID?
            Parameter2 = 1
        });
        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
    }
    
    private void HandleActionUnlock()
    {
        var player = this.Source.ToPlayer;
        player.SetMasterUnlock(this.ItemActionEntry.Data[0]);
        player.Inventory.DiscardItem(this.ItemContainer, this.ItemSlot);
        
    }
}