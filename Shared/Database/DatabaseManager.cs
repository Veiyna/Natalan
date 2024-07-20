using MongoDB.Bson.Serialization;
using Shared.Database.Authentication;
using Shared.Database.Datacentre;
using Shared.Database.World;

namespace Shared.Database
{
    public static class DatabaseManager
    {
        public static AuthenticationDatabase Authentication { get; } = new();
        public static DataCentreDatabase DataCentre { get; } = new();
        public static WorldDatabase World { get; } = new();

        public static void Initialise(ConfigDatabase authentication = null, ConfigDatabase dataCentre = null, ConfigDatabase world = null)
        {
            BsonClassMap.RegisterClassMap<CharacterInfo>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.GrandCompanyRanks).SetDefaultValue(new byte[3]);
                cm.MapMember(x => x.CompanionInfo).SetDefaultValue(new CharacterCompanionInfo());
                cm.MapMember(x => x.GlamourInfo).SetDefaultValue(new CharacterGlamourInfo());
                cm.MapMember(x => x.EurekaInfo).SetDefaultValue(new CharacterEurekaInfo());
            });
            
            if (authentication != null)
            {
                Authentication.Initialise(authentication.Host, authentication.Port, authentication.Username, authentication.Password, authentication.Database);
            }
            
            if (dataCentre != null)
            {
                DataCentre.Initialise(dataCentre.Host, dataCentre.Port, dataCentre.Username, dataCentre.Password, dataCentre.Database);
            }
            
            if (world != null)
            {
                World.Initialise(world.Host, world.Port, world.Username, world.Password, world.Database);
            }
        }
    }
}
