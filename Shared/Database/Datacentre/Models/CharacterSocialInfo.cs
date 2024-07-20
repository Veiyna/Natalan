using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre.Models
{
    [BsonIgnoreExtraElements]
    public class CharacterSocialInfo
    {
        public string SearchComment { get; set; } = "";
        public byte SelectRegion { get; set; } = 2;
    }
}