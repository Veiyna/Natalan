namespace WorldServer.Network
{
    public enum GmCommand
    {
        Pos = 0x0000,
        Lv = 0x0001,
        Call = 0x0007,
        Inspect = 0x0008,
        Speed = 0x0009,
        Kill = 0x000E,
        Raise = 0x0010,
        Icon = 0x0012,
        Hp = 0x0064,
        Mp = 0x0065,
        Tp = 0x0066,
        Gp = 0x0067,
        Exp = 0x0068,
        Item = 0x00C8,
        Gil = 0x00C9,
        QuestAccept = 0x012C,
        QuestCancel = 0x012D,
        QuestComplete = 0x012E,
        QuestIncomplete = 0x012F,
        QuestSequence = 0x0130,
        QuestInspect = 0x0131,
        GC = 0x0154,
        GCRank = 0x0155,
        Aetheryte = 0x15E,
        Wireframe = 0x0226,
        Teri = 0x258,
        Jail = 0x25A,
        Kick = 0x025C,
        TeriInfo = 0x025D,
        Jump = 0x025E,
        ImmediatelyAction = 0x264,
        FCPoint = 0x4B1,
        FCCredit = 0x4B2,
        FCRank = 0x4B3,
        EurekaStep = 0x9C4,
        
        
        
        
        
    }
}