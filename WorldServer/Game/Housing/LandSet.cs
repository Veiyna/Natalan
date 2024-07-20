using System.Collections.Generic;
using Lumina.Excel.GeneratedSheets;
using Shared.SqPack;
using WorldServer.Game.Housing.Enums;

namespace WorldServer.Game.Housing;

public class LandSet
{
    private const byte LandCount = 60;
    public ushort TerritoryId { get; set; }
    public ushort WardId { get; set; }
    public byte HousingIndex { get; set; }
    public List<Land> Lands { get; set; } = [];
    
    private HousingLandSet HousingLandSet;

    public LandSet(ushort territoryId, ushort wardId)
    {
        WardId = wardId;
        HousingIndex = HousingManager.TerritoryToIndex(territoryId);
        this.HousingLandSet = GameTableManager.HousingLandSet.GetRow(HousingManager.TerritoryToIndex(territoryId));
        TerritoryId = territoryId;

        for (ushort i = 0; i < LandCount; i++)
        {
            var landIdent = new LandIdent();
            landIdent.LandId = i;
            landIdent.TerritoryId = territoryId;
            landIdent.WorldId = 0;
            var land = new Land(landIdent, (HouseSize)this.HousingLandSet.PlotSize[i], HouseStatus.ForSale);
            Lands.Add(land);
        }
    }
}