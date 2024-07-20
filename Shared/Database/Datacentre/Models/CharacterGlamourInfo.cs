using System.Collections.Generic;
using Shared.Database.Datacentre.Models;

namespace Shared.Database.Datacentre;

public class CharacterGlamourInfo
{
    public List<GlamourDresserEntry> DresserEntries { get; set; } = new();
}