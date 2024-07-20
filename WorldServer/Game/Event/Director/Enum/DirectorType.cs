namespace WorldServer.Game.Event.Director.Enum;

public enum DirectorType : uint
{
    BattleLeve = 0x8001,
    GatheringLeve = 0x8002,
    InstanceContent = 0x8003, // used for dungeons/raids
    PublicContent = 0x8004,
    QuestBattle = 0x8006,
    CompanyLeve = 0x8007,
    TreasureHunt = 0x8009,
    GoldSaucer = 0x800A,
    CompanyCraftDirector = 0x800B,
    DpsChallange = 0x800D,
    Fate = 0x801A
}