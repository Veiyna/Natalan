using System.Collections;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Shared.SqPack;

namespace Shared.Database.Datacentre
{
    [BsonIgnoreExtraElements]
    public class CharacterProgressionInfo
    {
        public int BitsToBytes(uint bits)
        {
            return (int)(bits + 7) >> 3;
        }
        public void EnsureSize()
        {
            MasterMask.Length = 64 * 8;
            Aetheryte.Length = BitsToBytes(GameTableManager.Aetheryte.RowCount) * 8;
            HowTo.Length = BitsToBytes(GameTableManager.HowTo.RowCount) * 8;
            Minion.Length = BitsToBytes(GameTableManager.Companion.RowCount) * 8;
            Mount.Length = BitsToBytes((uint)GameTableManager.Mount.Max(row => row.Order)) * 8;
            Cutscene.Length = 154 * 8;//BitsToBytes(GameTableManager.CutsceneWorkIndex.Max(row => row.WorkIndex)) * 8;
            Discovery.Length = 480 * 8;
            Quest.Length = 727 * 8;
        }
        public BitArray MasterMask { get; set; }
        public BitArray Mount { get; set; }
        public BitArray Aetheryte { get; set; }
        public BitArray Discovery { get; set; }
        public BitArray HowTo { get; set; }
        public BitArray Minion { get; set; }
        public BitArray Cutscene { get; set; }
        public BitArray Quest { get; set; }
        
    }
}