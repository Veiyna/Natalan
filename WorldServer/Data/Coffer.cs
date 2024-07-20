using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorldServer.Data;

public class Coffer
{
    public uint ItemId;
    [JsonConverter(typeof(StringEnumConverter))]
    public CofferItemMode Mode;
    public List<uint> Items = [];
    public Coffer(uint itemId)
    {
        this.ItemId = itemId;
    }
}