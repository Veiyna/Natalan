using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre;

[BsonIgnoreExtraElements]
public class CharacterCompanionInfo
{
    public string Name { get; set; } = "";
    public byte Color { get; set; } = 36;
    public byte DefenseRank { get; set; }
    public byte AttackRank { get; set; }
    public byte HealerRank { get; set; }
}