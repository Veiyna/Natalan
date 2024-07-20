namespace WorldServer.Game.Event
{
    public enum EventHiType : ushort
    {
        Quest               = 0x0001,
        Warp                = 0x0002,
        GilShop             = 0x0004,
        Aetheryte           = 0x0005,
        GuildLeveAssignment = 0x0006,
        DefaultGossip       = 0x0009,
        CustomGossip        = 0x000B,
        ChocoboTaxiStand    = 0x0012,
        Opening             = 0x0013,
        GCShop              = 0x0016,
        SpecialShop         = 0x001B,
        InstanceContentGuide = 0x001D,
        HousingAethernet = 0x001E,
        SwitchGossip        = 0x001F,
        FccShop             = 0x002A,
        LotteryExchangeShop = 0x0034,
        DisposalShop        = 0x0035,
        PreHandler          = 0x0036,
        
    }
}
