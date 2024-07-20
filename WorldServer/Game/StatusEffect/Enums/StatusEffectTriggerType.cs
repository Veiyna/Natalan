namespace WorldServer.Game.StatusEffect.Enums
{
    public enum StatusEffectTriggerType : byte
    {
        None = 0,
        Apply = 1,
        Remove = 2,
        ApplyRemove = 3,
        Tick = 4,
        DamageReceive = 5,
        DamageDealt = 6,
        
    };
}