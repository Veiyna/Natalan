using MongoDB.Bson.Serialization.Attributes;
using Shared.SqPack;

namespace Shared.Database.Datacentre.Models
{
    [BsonIgnoreExtraElements]
    public class ItemModel
    {
        public ulong Id { get; set; }
        public ushort ContainerType { get;  set; }
        public ushort Slot { get; set; }
        public uint ItemId { get; set; }

        public uint StackSize { get; set; }
        
        public uint Glamour { get; set; }
        
        public ushort Color { get; set; }
        public ushort Color2 { get; set; }

        public ulong GetModel()
        {
            if (Slot == 0) return 0;
            var itemEntry = GameTableManager.Items.GetRow(ItemId);
            if (Glamour != 0) itemEntry = GameTableManager.Items.GetRow(Glamour);
            if (itemEntry == null) return 0;
            var model = itemEntry.ModelMain;
            model |= (uint)(this.Color << 24);

            return model;
        }
        
        
        
    }
}