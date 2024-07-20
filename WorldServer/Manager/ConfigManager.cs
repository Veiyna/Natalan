using System.IO;
using Shared;
using Shared.Database;

namespace WorldServer.Manager
{
    public struct WorldConfig
    {
        public struct ConfigDatabases
        {
            public ConfigDatabase Authentication { get; set; }
            public ConfigDatabase DataCentre { get; set; }
            public ConfigDatabase World { get; set; }
        }

        public struct ConfigServer
        {
            public int WorldPort { get; set; }
            public string AssetPath { get; set; }
        }

        public ConfigDatabases Database { get; set; }
        public ConfigServer Server { get; set; }
    }

    public static class ConfigManager
    {
        public static WorldConfig Config { get; private set; }

        public static void Initialise()
        {
            Config = JsonProvider.DeserializeObject<WorldConfig>(File.ReadAllText(@".\WorldConfig.json"));
        }
    }
}
