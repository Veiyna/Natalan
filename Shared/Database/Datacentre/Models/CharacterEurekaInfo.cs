using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre;

[BsonIgnoreExtraElements]
public class CharacterEurekaInfo
{
    public uint EurekaStep { get; set; }
    public uint ElementalLevel { get; set; } = 1;
}