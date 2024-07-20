using System.Collections.Generic;
using WorldServer.Game.StatusEffect.Enums;

namespace WorldServer.Data
{
    public class StatusEffectData
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        
        public List<StatusExecute> StatusEffectExecute { get; set; }
        
        public class StatusExecute
        {
            public StatusEffectType Type { get; set; }
            public StatusEffectTriggerType Trigger { get; set; }
            public uint Value { get; set; }
        }
        
    }
}