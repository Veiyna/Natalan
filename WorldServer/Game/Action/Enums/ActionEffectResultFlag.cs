namespace WorldServer.Game.Action.Enums
{
    public enum ActionEffectResultFlag : byte
    {
        None = 0,
        Absorbed = 0x04,
        ExtendedValue = 0x40,
        EffectOnSource = 0x80,
        Reflected = 0xA0,
    }
}