namespace Shared.Network
{
    public enum SubPacketClientHandlerId : ushort
    {
        None,
        ClientCharacterList,
        ClientEnterWorld,
        ClientLobbyRequest,
        ClientCharacterDelete,
        ClientCharacterCreate,
        ClientNewWorld,
        ClientTerritoryFinalise,
        ClientLogout,
        ClientPing,
        ClientContentFinderRequestInfo,
        ClientSocialBlackList,
        ClientSocialInvite,
        ClientSocialInviteResponse,
        ClientSocialList,
        ClientPartyLeave,
        ClientPartyDisband,
        ClientPartyKick,
        ClientPartyPromote,
        ClientActorAction,
        ClientActorActionEnvironment,
        ClientGmCommandInt,
        ClientGmCommandString,
        ClientPlayerMove,
        ClientInventoryAction,

        ClientZoneLine,
        ClientWorldVisit,
        ClientExamineFreeCompanyInfo,
        ClientExamineSearchComment,
        ClientViewSearchInfo,
        ClientInitSearchInfo,
        ClientSetSearchInfo,
        ClientAdventurerPlateView,
        ClientAdventurerPlateEdit,
        ClientEquipDisplayFlags,

        //Events
        //Event Trigger
        ClientEventGossip,
        ClientEventEmote,
        ClientEventAreaTrigger,
        ClientEventOutOfBounds,
        ClientEventTerritory,
        //Event Return
        ClientEventSceneFinish,
        ClientEventSceneFinish2,
        ClientEventSceneFinish3,
        //Event Yield
        ClientEventYield2,
        ClientEventYield16,
        ClientEventYieldString8,
        ClientEventYieldString16,
        ClientEventYieldString32,
        
        
        //Free Company
        ClientFreeCompanyInfo,
        ClientFreeCompanyDialog,
        ClientFreeCompanySlogan,
        ClientFreeCompanyParam,
        ClientFreeCompanyBoard,
        ClientFreeCompanyActivity,

        //Skills
        ClientSkill,
        ClientSkillAoE,

        ClientCFRegisterDuty,
        ClientCFCancel,
        ClientCFCommence,


        ClientUnk1,
        
        //Chat (WorldSession)
        ClientChat,
        ClientChannelJoin,
        //Chat (ChatSession)
        ClientChannelChat,
        ClientTell,
        
        //Trading
        ClientTrade,
        
        //Housing
        ClientHousingBuildPreset,
        
        //Bard Performance
        ClientPerformNote,
        
        //Novice Network
        ClientNoviceNetworkRequestJoin,
        
        //Dye (Dawntrail)
        ClientDye
        
    }
}