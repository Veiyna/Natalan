using System.Collections.Generic;
using System.IO;
using Shared.Database.Datacentre;
using Shared.Database.Datacentre.Models;
using Shared.Game;
using Shared.Network;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.StatusEffect;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerPlayerSpawn)]
    public class ServerPlayerSpawn : SubPacket
    {
        public byte SpawnIndex;
        public WorldPosition Position;
        public CharacterInfo Character;
        public FreeCompany FreeCompany;

        public ulong MainHandDisplayId;
        public ulong OffHandDisplayId;
        public IEnumerable<uint> VisibleItemDisplayIds;
        public IEnumerable<ushort> VisibleSecondDyeIds;
        
        public ulong TargetId;
        public Stance Stance;
        public OnlineStatus OnlineStatus;
        public byte CurrentPose;

        public uint MaxHP;
        public uint MaxMP;
        public uint HP;
        public uint MP;
        public byte State;
        public uint StateParam;
        public List<StatusEffect> StatusEffects;
        public uint DirectorId;
        public override void Write(BinaryWriter writer)
        {
            
            if (StateParam == 0 && State == 11) //
                State = 1;
            var displayFlags = (uint) this.Stance;
            if ((this.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.HideHead) != 0)
                displayFlags |= (ushort)DisplayFlags.HideHead;
            if ((this.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.HideWeapon) != 0)
                displayFlags |= (ushort)DisplayFlags.HideWeapon;
            if ((this.Character.EquipDisplayFlags & (byte)EquipDisplayFlags.Visor) != 0)
                displayFlags |= (ushort)DisplayFlags.Visor;

            if (Version.Version.StartsWith("7")) // DAWNTRAIL
            {
                writer.Write((ulong)this.Character.AccountId);
                writer.Write(this.Character.Id);
            }
                
            writer.Write(this.Character.CurrentTitle);    // title id
            writer.Write((ushort)0);
            writer.Write(Character.CurrentRealmId);
            writer.Write(Character.RealmId);
            writer.Write((byte)90);      // enables GM commands
            writer.Write((byte)1);
            writer.Write((byte)1);
            writer.Write((byte)this.OnlineStatus);     // online Status
            writer.Write(this.CurrentPose);      // pose id
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(TargetId);          // target
            writer.Write(0u);
            writer.Write(0u);

            // models
            writer.Write(MainHandDisplayId);
            writer.Write(OffHandDisplayId);
            writer.Write(0ul);          // crafting

            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(0u);
            writer.Write(this.DirectorId);
            writer.Write(0xE0000000u);
            writer.Write(0xE0000000u);

            
            writer.Write(MaxHP);         // max HP
            writer.Write(HP);         // HP
            writer.Write(displayFlags);
            writer.Write((ushort)0);
            writer.Write((ushort)MP);  // MP
            writer.Write((ushort)MaxMP);  // max MP
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            
            writer.Write(Position.PackedOrientationShort);
            writer.Write((ushort)this.Character.Mount);
            writer.Write(this.Character.Companion);
            writer.Write((byte)0); // ORNAMENT
            
            
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);

            writer.Write(SpawnIndex);
            writer.Write(State);      // state (1 = alive, 2 = dead, 3 = sitting)
            writer.Write((byte)StateParam);

            
            writer.Write((byte)1);      // type (1 = Player, 2 = NPC, 3 = ??)
            writer.Write((byte)4);
            writer.Write(Character.Voice);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)Character.Class.Level);
            writer.Write(Character.ClassJobId);
            writer.Write((ushort)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(this.Character.CompanionInfo.Color);
            writer.Write((byte)0);
            writer.Write((byte)this.Character.EurekaInfo.ElementalLevel); // elemental level
            writer.Pad(5u);
            
            


            // status effect
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
            writer.Write(Position.Offset.X);
            writer.Write(Position.Offset.Y);
            writer.Write(Position.Offset.Z);
            

            
            foreach (uint displayId in VisibleItemDisplayIds)
                writer.Write(displayId);
            

            

            foreach (uint displayId in VisibleSecondDyeIds)
                writer.Write((byte)displayId);
            
            //DAWNTRAIL
            if (Version.Version.StartsWith("7"))
            {
                writer.Write(this.Character.Glasses); // FACEWEAR
            }
                
            writer.WriteStringLength(Character.Name, 0x20u);
            
            writer.Write(Character.Appearance.Data);
            writer.WriteStringLength(this.FreeCompany?.Tag ?? "", 6u);
            writer.Pad(6u);
        }
    }
}
