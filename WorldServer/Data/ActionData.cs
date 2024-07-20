using System.Collections.Generic;
using WorldServer.Data.Enum;

namespace WorldServer.Data
{
    public class ActionData
    {

        public class StatusData
        {
            public uint Id { get; set; }
            public string Name { get; set; }
            public float Duration { get; set; }
            public uint Param { get; set; }

            public Dictionary<string, object> Flags { get; set; } = new();
        }

        public class PotencyData
        {
            public uint Base { get; set; }
            public uint Combo { get; set; }

            public uint Positional { get; set; }
            public uint PositionalCombo { get; set; }

            public uint Heal { get; set; }
            public uint SelfHeal { get; set; }

            public uint MPGainPercentage { get; set; }
        }

        public uint Id { get; set; }
        public string Name { get; set; }

        public PositionalDirection PositionalDirection { get; set; }

        public PotencyData Potency { get; set; } = new();

        public List<StatusData> Statuses { get; set; } = [];
    }
}