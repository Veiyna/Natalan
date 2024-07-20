using System;
using LobbyServer.Manager;
using Shared.Command;
using Shared.Database;
using Shared.Network;
using Shared.SqPack;

namespace LobbyServer
{
    internal static class LobbyServer
    {
        #if DEBUG
            public const string Title = "Lobby Server (Debug)";
        #else
            public const string Title = "Lobby Server (Release)";
        #endif

        private static void Main()
        {
            Console.Title = Title;
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Shutdown();

            ConfigManager.Initialise();
            DatabaseManager.Initialise(ConfigManager.Config.Database.Authentication, ConfigManager.Config.Database.DataCentre, ConfigManager.Config.Database.World);
            GameTableManager.InitialiseLobby(ConfigManager.Config.Server.AssetPath);
            PacketManager.Initialise();
            NetworkManager.Initialise(ConfigManager.Config.Server.LobbyPort);
            AssetManager.Initialise();
            UpdateManager.Initialise();
            CommandManager.Initialise();
        }

        public static void Shutdown()
        {
            NetworkManager.Shutdown = true;
            // remaining managers are background threads
        }
    }
}
