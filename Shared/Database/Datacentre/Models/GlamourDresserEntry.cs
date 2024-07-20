namespace Shared.Database.Datacentre.Models;

public class GlamourDresserEntry
{
    public uint ItemId;
    public ushort Color;
    public GlamourDresserEntry(uint itemId, ushort color)
    {
        this.ItemId = itemId;
        this.Color = color;
    }
}