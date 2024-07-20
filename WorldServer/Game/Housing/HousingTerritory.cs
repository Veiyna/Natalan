using Lumina.Excel.GeneratedSheets;
using WorldServer.Game.Entity;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Game.Housing;

public class HousingTerritory : Territory
{
    public HousingTerritory(TerritoryType entry, LandSet landSet) : base(entry)
    {
        this.LandSet = landSet;
    }
    public LandSet LandSet;

    public void SendLandUpdate(Land land)
    {
        SendToAll(new ServerLandUpdate
        {
            Land = land
        });
    }
    
    public override void OnZoneIn(Player player)
    {
        HousingSetup(player);
    }


    private void HousingSetup(Player player)
    {

        SendLandSet(player);

        for (byte i = 0; i < 8; i++)
        {
            player.Session.Send(new ServerHousingObjectInitialize {
                Index = i
            });
            
        }
        player.Session.Send(new ServerLandSetMap
        {
            LandSet = this.LandSet
        });
    }

    private void SendLandSet(Player player)
    {
        player.Session.Send(new ServerLandSetInitialize
        {
            player = player,
            LandSet = this.LandSet,
        });
    }


    
}