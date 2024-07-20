namespace Shared.Network
{
    public enum SubPacketServerHandlerId : ushort
    {
        None,
        ServerError,
        ServerServiceAccountList,
        ServerCharacterList,
        ServerCharacterCreate,
        ServerEnterWorld,
        ServerRealmList,
        ServerRetainerList,
        ServerNewWorld,
        ServerLogout,
        ServerPing,
        ServerSocialInviteResponse,
        ServerSocialMessage,
        ServerSocialInviteUpdate,
        ServerSocialList,
        ServerMessage,
        ServerMessage2,
        ServerSocialAction,
        ServerSocialBlacklist,
        ServerSocialBlacklistOld,
        ServerPlayerSpawn,
        ServerNpcSpawn,
        ServerObjectSpawn,
        ServerPlayerStats,
        ServerActorDespawn,
        ServerActorSetPos,
        ServerActorMove,
        ServerActorCast,
        ServerParty,
        ServerPlayerSetup,
        ServerPlayerStateFlags,
        ServerClassSetup,
        ServerActorAppearanceUpdate,
        //Inventory
        ServerItemSetup,
        ServerContainerSetup,
        ServerInventoryAction,
        ServerInventoryUpdateFinish,
        ServerInventoryUpdate,
        ServerItemUpdate,

        // all 3 invoke the same client function just with different parameters
        ServerActorAction,
        ServerActorActionSelf,
        ServerActorActionTarget,
        //Events
        ServerEventSceneStart,
        ServerEventSceneStart255,
        ServerEventStart,
        ServerEventStop,
        ServerEventYield,
        ServerEventYield32,

        ServerTerritorySetup,
        ServerAchievementList,
        ServerContentFinderList,
        ServerUnknown0207,
        ServerUnknown0209,
        ServerTerritoryPending,
        ServerWorldVisit,
        ServerTitleList,
        ServerExamine,
        ServerExamineFreeCompanyInfo,
        ServerExamineSearchComment,
        ServerSetOnlineStatus,

        //Quests
        ServerQuestJournalActiveList,
        ServerQuestJournalCompleteList,
        
        ServerQuestUpdate,
        ServerQuestTracker,
        ServerQuestFinish,
        ServerMSQTrackerComplete,
        ServerMSQTrackerProgress,

        //Search Info
        ServerViewSearchInfo,
        ServerInitSearchInfo,
        ServerUpdateSearchInfo,

        //Adventurer Plate
        ServerAdventurerPlateView,


        ServerEquipDisplayFlags,

        //Messages
        ServerLogMessage,
        ServerSystemLogMessage,
        ServerErrorMessage,
        ServerLootMessage,
        ServerShopMessage,
        ServerQuestMessage,

        ServerUpdateHpMpTp,
        ServerUpdateClassInfo,

        ServerMount,

        //Action
        ServerEffect,
        ServerAoeEffect8,
        ServerEffectResult,
        //Status Effect
        ServerStatusEffectList,

        ServerJobGauge,

        
        //Free Company
        ServerFreeCompanyInfo,
        ServerFreeCompanyDialog,
        ServerFreeCompanySlogan,
        ServerFreeCompanyParam,
        ServerFreeCompanyBoard,
        ServerFreeCompanyActivity,

        //Enmity
        ServerHateList,
        ServerHateRank,
        //Content Finder
        ServerCFRegister,
        ServerCFCancel,
        ServerContentFinderMemberStatus,
        ServerContentFinderNotify,
        ServerCharaVisualEffect,
        ServerContentFinderPlayerInNeed,
        ServerContentFinderDutyInfo,
        ServerContentFinderRegister,

        
        //Unknown (Seems to allow cutscene replay)
        ServerUnk1,
        
        
        
        //Novice Network
        ServerNoviceNetworkId,
        ServerNoviceNetworkJoined,
        
        //Housing
        
        ServerLandSetInitialize,
        ServerLandSetMap,
        ServerHousingObjectInitialize,
        ServerHousingUpdateLandFlagsSlot,
        ServerHousingLandFlags,
        ServerLandAvailability,
        ServerLandPriceUpdate,
        ServerLandUpdate,
        ServerLandInfoSign,
        ServerHousingUnknown,
        
        
        //Chat (WorldSession)
        ServerChat,
        //Chat (ChatSession)
        ServerChannelChat,
        ServerTell,
        ServerTellNotFound,
        
        //Trade
        ServerTrade,
        
        //Bard Performance
        ServerPerformNote,
        
        //Director
        ServerDirectorVars,
        
        //Grand Company
        ServerGCAffiliation,
        
        //Chocobo
        ServerCompanionName,
        
        //Glamour
        ServerGlamourDresser,
        ServerGlamourPlate,


    }
}