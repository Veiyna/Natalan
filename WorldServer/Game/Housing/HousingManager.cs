using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Housing.Enums;
using WorldServer.Game.Map;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Housing;

public static class HousingManager
{
    private const byte WardCount = 30;
    private static List<LandSet> landSets { get; set; } = [];
    private static List<HousingTerritory> HousingTerritories { get; set; } = [];
    public static void Initialise()
    {
        for (byte i = 0; i < 5; i++)
        {
            for (ushort j = 0; j < WardCount; j++)
            {
                var territoryId = (ushort)IndexToTerritory(i);
                var landset = new LandSet(territoryId, j);
                landSets.Add(landset);

                var territoryEntry = GameTableManager.TerritoryTypes.GetRow(territoryId);
                var territory = new HousingTerritory(territoryEntry, landset);
                MapManager.RegisterTerritory(territory);
                HousingTerritories.Add(territory);

            }
        }
    }

    public static byte TerritoryToIndex(uint territoryId)
    {
        return territoryId switch
        {
            339 => 0,
            340 => 1,
            341 => 2,
            641 => 3,
            979 => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(territoryId), territoryId, null)
        };
    }

    public static void SendLandSignFree(Player player, LandIdent ident)
    {
        player.ActiveLand = ident;
        player.Session.Send(new ServerLandPriceUpdate
        {
            Land = LandIdentToLand(ident)
        });
    }
    
    public static void SendLandSignOwned(Player player, LandIdent ident)
    {
        player.ActiveLand = ident;
        player.Session.Send(new ServerLandInfoSign
        {
            Land = LandIdentToLand(ident)
        });
    }

    public static LandPurchaseResult PurchaseLand(Player player, Land land)
    {
        var gil = player.GetCurrency(CurrencyType.Gil);
        var housingTerritory = (HousingTerritory)player.Map;
        if (land == null)
            return LandPurchaseResult.ERR_INTERNAL;
        if (land.HouseState != HouseStatus.ForSale)
            return LandPurchaseResult.ERR_NOT_AVAILABLE;
        if (gil < land.Price)
            return LandPurchaseResult.ERR_NOT_ENOUGH_GIL;

        land.HouseState = HouseStatus.Sold;
        land.LandType = LandType.Private;
        
        housingTerritory.SendLandUpdate(land);
        
        player.SetHousingAccess(LandFlagsSlot.Private, LandFlags.HasAetheryte, land.LandIdent);
        
        
        return LandPurchaseResult.SUCCESS;
    }
    public static Land LandIdentToLand(LandIdent landIdent)
    {
        return landSets.FirstOrDefault(t => t.TerritoryId == landIdent.TerritoryId && t.WardId == landIdent.WardNumber)
            ?.Lands
            .FirstOrDefault(l => l.LandIdent.Equals(landIdent));
    }

    public static void TeleportToWard(Player player, byte index, byte ward)
    {
        var territory = HousingTerritories.FirstOrDefault(h => h.LandSet.HousingIndex == index && h.LandSet.WardId == ward);
        player.TeleportTo(new WorldPosition((ushort)territory.Entry.RowId, Vector3.Zero, 0, territory.InstanceId));
    }

    public static uint IndexToTerritory(byte index)
    {
        return index switch
        {
            0 => 339,
            1 => 340,
            2 => 341,
            3 => 641,
            4 => 979,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
        };
    }

    public static LandIdent ParamsToLandIdent(uint param1, uint param2, bool use16 = true)
    {
        LandIdent ident;
        ident.WorldId = (ushort)( param1 >> 16 );
        ident.TerritoryId = (ushort)( param1 & 0xFFFF );

        if( use16 )
        {
            ident.WardNumber = (ushort)( param2 >> 16 );
            ident.LandId = (ushort)( param2 & 0xFFFF );
        }
        else
        {
            ident.WardNumber = (ushort)((param2 >> 8) & 0xFF);
            ident.LandId = (ushort)(param2 & 0xFF);
        }

        return ident;
    }


public static uint ToLandSetId( ushort territoryTypeId, byte wardId )
    {
        return (uint)(( territoryTypeId << 16 ) | wardId);
    }


    public static void BuildEstate(Player player, byte plotId, uint itemId)
    {
        var housingTerritory = (HousingTerritory)player.Map;

        var land = housingTerritory.LandSet.Lands[plotId];

        
        land.House = new House();

        land.HouseState = HouseStatus.PrivateEstate;
        
        player.Session.Send(new ServerActorAction
        {
            Action = ActorActionServer.BuildPresetResponse,
            Parameter1 = plotId
        });
        housingTerritory.SendLandUpdate(land);
        player.SetHousingAccess(LandFlagsSlot.Private, LandFlags.HasAetheryte | LandFlags.EstateBuilt, land.LandIdent);
        
    }
    
    public static void RemoveEstate(Player player, byte plotId)
    {
        var housingTerritory = (HousingTerritory)player.Map;

        var land = housingTerritory.LandSet.Lands[plotId];


        land.House = null;

        land.HouseState = HouseStatus.Sold;
        
        player.Session.Send(new ServerActorAction
        {
            Action = ActorActionServer.BuildPresetResponse,
            Parameter1 = plotId
        });
        housingTerritory.SendLandUpdate(land);
        player.SetHousingAccess(LandFlagsSlot.Private, LandFlags.HasAetheryte, land.LandIdent);
        
    }
}