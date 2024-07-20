using WorldServer.Game.Housing.Enums;

namespace WorldServer.Game.Housing;

public class Land
{
    public LandIdent LandIdent;
    public House House { get; set; }
    public HouseSize HouseSize { get; set; }
    public HouseStatus HouseState { get; set; }
    public LandType LandType { get; set; }
    public uint Price { get; set; } = 1;

    public Land(LandIdent landIdent, HouseSize houseSize, HouseStatus houseState)
    {
        this.LandIdent = landIdent;
        this.HouseSize = houseSize;
        this.HouseState = houseState;
    }

}