namespace Shared.Database.World.Models;
public class CharacterSpawn
{
    public byte cityStateId { get; set; }
    public ushort territoryId { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float o { get; set; }
}