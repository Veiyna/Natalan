using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre;

[BsonIgnoreExtraElements]
public class CharacterClassInfo
{
    public const byte MaxClassId = 32;
    public byte classId { get; set; }
    public ushort Level { get; set; }
    public uint Experience { get; set; }
        

    public CharacterClassInfo(byte id)
    {
        classId = id;
        Level      = 0;
        Experience = 0;
    }
        
    public CharacterClassInfo()
    {
    }
        
}