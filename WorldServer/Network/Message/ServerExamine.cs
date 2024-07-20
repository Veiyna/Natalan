using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerExamine)]
public class ServerExamine : SubPacket
{
    public Player Player { get; set; }
    public override void Write(BinaryWriter writer)
    {
            var displayFlags = (byte)0;
            
            if ((Player.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.HideHead) != 0)
                displayFlags |= (byte)ExamineDisplayFlags.HideHead;
            if ((Player.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.HideWeapon) != 0)
                displayFlags |= (byte)ExamineDisplayFlags.HideWeapon;
            if ((Player.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.Visor) != 0)
                displayFlags |= (byte)ExamineDisplayFlags.Visor;
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(Player.Character.ClassJobId);
            writer.Write((byte)Player.Character.Class.Level);
            writer.Pad(2u);
            writer.Write(Player.Character.CurrentTitle);
            writer.Write(Player.Character.GrandCompany);
            writer.Write(Player.Character.CurrentGrandCompanyRank);
            writer.Write(displayFlags); // display flags
            writer.Pad(5u);
            writer.Pad(4u);
            writer.Pad(4u);
            writer.Pad(8u);
            writer.Write(Player.Inventory.GetWeaponDisplayIds().MainHand);
            writer.Write(Player.Inventory.GetWeaponDisplayIds().OffHand);
            writer.Pad(2u);
            writer.Write(Player.Character.RealmId);
            writer.Pad(28u);
            //writer.Pad(560u);
            for (byte i = 0; i <= (byte)ContainerEquippedSlot.SoulCrystal; ++i)
            {
                var item = Player.Inventory.GetItemAtSlot(ContainerType.Equipped, (ContainerEquippedSlot)i);
                if (item != null)
                {
                    writer.Write(item.Entry.RowId);
                    writer.Write(item.Glamour);
                    writer.Pad(8u);
                    writer.Pad(1u);
                    writer.Write((byte)item.Color);
                    writer.Pad(2u);
                    writer.Pad(20u);
                }
                else
                {
                    writer.Pad(40u);
                }
                

            }
            writer.WriteStringLength(Player.Character.Name, 0x20);
            writer.Pad(1u);
            writer.Pad(31u);
            writer.Write(Player.Character.Appearance.Data);
            writer.Pad(2u);
            foreach (uint displayId in Player.Inventory.GetVisibleItemDisplayIds())
                writer.Write(displayId);
            writer.Pad(156u);
        }
}