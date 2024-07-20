namespace WorldServer.Game.StatusEffect.Enums
{
    public enum StatusEffectType : byte
    {
        Invalid = 0,
        Heal = 1,
        Damage = 2,
        DamageMultiplier = 3,
        DamageReceiveMultiplier = 4,
        HealReceiveMultiplier = 5,
        HealCastMultiplier = 6,
        CritDHRateBonus = 7,
        Shield = 10,
        MPRestore = 11,
        Haste = 12,
        InstantCast = 13,
        BlockParryRateBonus = 14,
        MPRestorePerGCD = 15,
        AlwaysCombo = 16,
        PotencyMultiplier = 17,
        GaugeToggle = 18
    };
}