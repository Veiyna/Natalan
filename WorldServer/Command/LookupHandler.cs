using System;
using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets;
using Shared.Command;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Network;

namespace WorldServer.Command
{
    public static class LookupHandler
    {
        // lookup_territory name/partial name
        [CommandHandler("lookup_territory", SecurityLevel.Developer)]
        public static void HandleActorTeleport(WorldSession session, params string[] parameters)
        {
            if (parameters.Length == 0)
                return;

            string searchString = string.Join(" ", parameters);

            var matches = new List<(uint Index, string Name)>();
            foreach (TerritoryType territoryEntry in GameTableManager.TerritoryTypes)
            {
                if (territoryEntry.Name == string.Empty)
                    continue;

                string placeName = territoryEntry.PlaceName.ToString();
                if (placeName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                    matches.Add((territoryEntry.RowId, placeName));
            }

            foreach ((uint Index, string Name) match in matches)
                Console.WriteLine($"Match: {match.Index} - {match.Name}");
        }
    }
}
