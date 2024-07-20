using Shared.Game.Enum;

namespace Shared.Database.Authentication
{
    public class ServiceAccountInfo
    {
        public uint Id { get; set; }
        public uint AccountId { get; set; }
        public string Name { get; set; }
        public Expansion Expansion { get; set; } = Expansion.Dawntrail;
        public ushort RealmCharacterLimit { get; set; } = 8;
        public ushort AccountCharacterLimit { get; set; } = 40;

        public ServiceAccountInfo()
        {
        }
    }
}
