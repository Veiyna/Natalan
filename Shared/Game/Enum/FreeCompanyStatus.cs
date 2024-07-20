namespace WorldServer.Game.FreeCompany.Enums
{
    public enum FreeCompanyStatus : byte
    {
        Invalid = 0x0,
        InviteStart = 0x1,
        InviteComplete = 0x2,
        Normal = 0x3,
        Freeze = 0x4,
    };
}