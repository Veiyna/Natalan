using System;
using System.Collections.Generic;
using System.Linq;
using Lumina.Excel.GeneratedSheets;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Entity;
using WorldServer.Game.StatusEffect.Enums;

namespace WorldServer.Game.StatusEffect
{
    public class StatusEffect
    {
        public uint StatusId;
        public Status StatusEntry;

        public StatusEffectData ServerStatusEntry;

        public Character Source;
        public Character Target;
        public float Duration;
        public float TimeLeft => this.Duration - ((ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds() - this.StartTime)/1000 ?? this.Duration;
        public ulong? StartTime;
        private uint TickRate = 3000;
        public ushort Param;
        public Dictionary<StatusEffectTriggerType, List<StatusEffectData.StatusExecute>> Effects = new();
        public StatusEffect(uint id, Character source, Character target, float duration, ushort param)
        {
            StatusId = id;
            Source = source;
            this.Target = target;
            this.Duration = duration;
            this.Param = param;
            this.StatusEntry = GameTableManager.Status.GetRow(id);
            this.ServerStatusEntry = DataManager.GetStatusEffect(id);
        }


        public void OnExecute()
        {
            Console.WriteLine($"Status effect {this.StatusId}:{this.StatusEntry.Name} applied to {this.Target.ToPlayer?.Character.Name}");
            if (this.ServerStatusEntry is not null)
            {
                var applicable =
                    this.ServerStatusEntry?.StatusEffectExecute.Where(s => s.Trigger is StatusEffectTriggerType.Apply or StatusEffectTriggerType.ApplyRemove);
                foreach (var statusExecute in applicable)
                {
                    StatusEffectManager.ExecuteEffect(this, statusExecute);
                }
            }
            

        }

        public void OnTick()
        {
            if (this.ServerStatusEntry is not null)
            {
                var applicable =
                    this.ServerStatusEntry?.StatusEffectExecute.Where(s => s.Trigger is StatusEffectTriggerType.Tick);
                foreach (var statusExecute in applicable)
                {
                    StatusEffectManager.ExecuteEffect(this, statusExecute);
                }
            }
        }
        public void OnRemove()
        {
            if (this.ServerStatusEntry is not null)
            {
                var applicable =
                    this.ServerStatusEntry.StatusEffectExecute.Where(s =>
                        s.Trigger is StatusEffectTriggerType.Remove or StatusEffectTriggerType.ApplyRemove);

                foreach (var statusExecute in applicable)
                {
                    StatusEffectManager.ExecuteEffect(this, statusExecute);
                }
            }
        }
    }
}