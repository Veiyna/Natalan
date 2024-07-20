namespace WorldServer.Game.Entity.Enums;

public enum ActorStatus : byte
{
    Idle = 0x01,
    Dead = 0x02,
    Sitting = 0x03,
    Mounted = 0x04,
    Crafting = 0x05,
    Gathering = 0x06,
    Melding = 0x07,
    SMachine = 0x08,
    Carry = 0x09,
    Pillon = 0x0A,
    EmoteMode = 0x0B,
    Performing = 0x10
}