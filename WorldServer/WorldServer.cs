using System;
using Shared.Command;
using Shared.Database;
using Shared.Network;
using Shared.SqPack;
using WorldServer.Data;
using WorldServer.Game.Action;
using WorldServer.Game.FreeCompany;
using WorldServer.Game.Housing;
using WorldServer.Game.Map;
using WorldServer.Game.Social;
using WorldServer.Game.StatusEffect;
using WorldServer.Manager;
using WorldServer.Network;
using WorldServer.Script;

namespace WorldServer
{
    internal static class WorldServer
    {
        #if DEBUG
            public const string Title = "World Server (Debug)";
        #else
            public const string Title = "World Server (Release)";
        #endif

        private static void Main()
        {
            Console.Title = Title;
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Shutdown();

            ConfigManager.Initialise();
            DatabaseManager.Initialise(ConfigManager.Config.Database.Authentication, ConfigManager.Config.Database.DataCentre, ConfigManager.Config.Database.World);
            GameTableManager.InitialiseWorld(ConfigManager.Config.Server.AssetPath);
            PacketManager.Initialise();
            ActorActionManager.Initialise();
            GmCommandManager.Initialise();
            AssetManager.Initialise();
            DataManager.Initialise();
            ScriptManager.Initialise();
            MapManager.Initialise();
            SocialManager.Initialise();
            FreeCompanyManager.Initialise();
            ActionManager.Initialise();
            StatusEffectManager.Initialise();
            HousingManager.Initialise();
            NetworkManager.Initialise(ConfigManager.Config.Server.WorldPort);
            UpdateManager.Initialise();
            CommandManager.Initialise();
        }

        public static void Shutdown()
        {
            // remaining managers are background threads
            NetworkManager.Shutdown = true;
            foreach (var player in MapManager.GetPlayers())
            {
                player.SavePlayerData();
            }
            
        }
    }
}
