using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets;
using Shared;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Map;

namespace WorldServer.Game.Navigation;

public static class NavigationManager
{
    private static Dictionary<string, NavigationProvider> NavigationProviders;
    public static void Initialise()
    {
        foreach (TerritoryType entry in GameTableManager.TerritoryTypes)
        {
            if (entry.Name == string.Empty)
                continue;

            var name = entry.GetBgName();
            var navMesh = DataManager.GetNavMesh(name);
            if (navMesh != null)
            {
                NavigationProviders.Add(name,new NavigationProvider(navMesh));
            }
        }
    }
}