using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerNpcSpawn)]
public class ServerNpcSpawn : SubPacket
{
    public byte SpawnIndex;
    public BNpc BNpc;
    public override void Write(BinaryWriter writer)
    {
        writer.Write(this.BNpc.GimmickId);    // gimmick id
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write(this.BNpc.AggressionMode); // aggression mode
        writer.Write((byte)0); 
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((uint)0);
        writer.Write(this.BNpc.targetId);     // target
        writer.Write(0u);
        writer.Write(0u);

        // models
        writer.Write(this.BNpc.WeaponMain);
        writer.Write(this.BNpc.WeaponSub);
        writer.Write(0ul);          // crafting

        writer.Write(0u);
        writer.Write(0u);
        writer.Write(this.BNpc.BNpcBaseId);
        writer.Write(this.BNpc.BNpcNameId);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(0u);
        writer.Write(this.BNpc.TriggerOwnerId);
        writer.Write(0xE0000000u);

        writer.Write(this.BNpc.MaxHP);         // max HP
        writer.Write(this.BNpc.HP);         // HP
        writer.Write(this.BNpc.DisplayFlags);
        writer.Write((ushort)0);
        writer.Write((ushort)this.BNpc.MP);  // MP
        writer.Write((ushort)this.BNpc.MaxMP);  // max MP
        writer.Write((ushort)0);
        writer.Write(this.BNpc.ModelChara);

        writer.Write(this.BNpc.Position.PackedOrientationShort);
        writer.Write((ushort)0);
        writer.Write((ushort)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((ushort)0);
        writer.Write(SpawnIndex);
        writer.Write((byte)this.BNpc.State);      // state (1 = alive, 2 = dead, 3 = sitting)
        writer.Write((byte)0);
        writer.Write((byte)2);      // type (1 = Player, 2 = NPC, 3 = ??)
        writer.Write((byte)5);
        writer.Write((byte)0);
        writer.Write(BNpc.ClassJobId);
        writer.Write(this.BNpc.EnemyType);
        writer.Write(this.BNpc.Level);
        writer.Write((byte)0);
        writer.Write((ushort)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Write((byte)0);
        writer.Pad(6u);
        writer.Write((byte)0);

        // aura
        var StatusEffects = this.BNpc.StatusEffects;
        for (var i = 0; i < 30; i++)
        {
            if (i < StatusEffects.Count)
            {
                var effect = StatusEffects[i];
                writer.Write((ushort)effect.StatusId);
                writer.Write(effect.Param);
                writer.Write(effect.TimeLeft);
                writer.Write(effect.Source.Id);
            }
            else
                writer.Pad(12u);
        }

        writer.Write(this.BNpc.Position.Offset.X);
        writer.Write(this.BNpc.Position.Offset.Y);
        writer.Write(this.BNpc.Position.Offset.Z);
        for (int i = 0; i < this.BNpc.ModelEquip.Length; i++)
        {
            writer.Write(this.BNpc.ModelEquip[i]);
        }
        writer.Pad(10u);

        writer.WriteStringLength("", 0x20u);
        writer.Write(this.BNpc.Customize);
        writer.WriteStringLength("", 6u);
        writer.Pad(6u);
    }
}