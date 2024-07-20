namespace WorldServer.Network
{
    public enum ActorActionServer : ushort
    {
        ToggleWeapon = 0x00,
        SetStatus = 0x02,
        ClassJobChange = 0x05,
        SetBattle = 0x04,
        GainExpMsg = 0x07,
        ClassJobUpdate = 0x9,
        LevelUpEffect = 0x0A,
        ActionStart = 0x11,
        DeathAnimation = 0x0E,
        StatusEffectGain = 0x14,
        StatusEffectLose = 0x15,
        HPFloatingText = 0x17,
        Flee = 0x1B,
        Size = 0x1F,
        ToggleMasterUnlock = 0x29,
        UpdateUiExp = 0x2B,
        SetTarget                   = 0x32,
        DirectorInit = 0x64,
        DirectorClear = 0x65,
        DirectorEObjMod = 0x6A,
        DirectorUpdate = 0x6D,
        RemoveAura                  = 0x0068,
        ItemCreate                  = 0x0075,
        DutyQuestScreenMsg = 0x7B,
        DutyUnlock = 0x83,
        ZoneIn                      = 0xC8,
        TeleportStart               = 0xCB,
        PartyTeleport = 0xCC,
        ActorDespawnEffect          = 0xD4,
        MountEnable1                = 0x107,
        ToggleCompanion             = 0x10E,
        Titles                      = 0x012F,
        AttributeAllot              = 0x0135,
        HuntingLog                  = 0x0194,
        EObjSetState = 0x199,
        Dye                         = 0x1B1,
        Glam                        = 0x1B6,
        EstateTimers                = 0x01AB,
        SetHomepoint                = 0x1FB,
        AetheryteUnlock = 0x1FD,
        SetItemLevel                = 0x209,
        ReturnersBounty             = 0x216,
        AchievementCriteriaResponse = 0x0202,
        AchievementComplete         = 0x0203,
        AchievementCompleteChat     = 0x0206,
        ToggleWireframeRendering    = 0x261,
        SetMountBitmask = 0x387,
        MountEnable2                = 0x393,
        SetMountSpeed               = 0x3A0,
        Dismount                    = 0x3A2,
        FramerKitUnlock = 0x3AE,
        GlassesChange = 0x3b0,
        GlassesUnlock = 0x3b1,
        AchievementCriteriaRequest  = 0x03E8,
        AchievementList             = 0x03E9,
        ShowHousingItemUI = 0x3F7,
        ShowBuildPresetUI = 0x3E9,
        BuildPresetResponse = 0x3ED,
        IndivdualTimers             = 0x0515,
        SomeCutsceneUnk             = 0x40e,
        HPFloatingText2             = 0x603,
        CompanionAction             = 0x06A4,
        CompanionUpdateGear         = 0x06A5,
        CompanionNewAction          = 0x06A6,
        HallOfTheNoviceBegin        = 0x0802,
        
        
        SetPose = 0x127,
        
        Emote = 0x122,
        EmoteInterrupt = 0x123,
        
        SetTitle = 0x1F4,
        
        SetStatusIcon = 0x1F8,
        
        SetCharaGearParamUI = 0x260,
        
        
        
        
        AdventurerPlateEditResponse = 0xc1e,
        
        FantasiaMsg = 0x388,
        
        SetNewAdventurer = 0x38C, // new adventurer on = param2 0, off = param2 1, used for a lot more----- param1 - flag type, param2 - value
        EventBattleDialog = 0x39D,
        SetMentor = 0x3A7, // param1 - 2?
        
        GoldSaucerPrize = 0x38E,
        
        CastInterrupt = 0x0F,
        
        
        TeleportReq = 0xCC, //param1 = show stuff 0-1 param2 aetheryte id param3 idk 0-1
        
        LogMsg = 0x205,
        
        GlamourDresserUpdate = 0x708, // param1 = slot, param2 = itemId
        GlamourDresserApplied = 0x709, // param1 = 1??
        
        
        
        EurekaStep = 0x73A, // alters the progress of the player on all Eureka maps
        
        IslandSanctuaryCurrencyInfo = 0xC82,
        IslandSanctuaryUnk = 0xC83,
        IslandSanctuaryReleaseMinion = 0xC91,
        IslandSanctuaryRetrieveMinion = 0xC92,
        IslandSanctuaryRecallMinions = 0xC93,
        IslandSanctuaryOrchestrion = 0xCA1,

        Action00C9 = 0xC9,
    }
}
