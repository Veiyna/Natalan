using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre.Models
{
    [BsonIgnoreExtraElements]
    public class CharacterAdventurerPlate
    {
        public byte[] RawData { get; set; }
    }
}