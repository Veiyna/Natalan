﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.Database;
using Shared.Database.Authentication;
using Shared.Database.Datacentre;
using Shared.Game;

namespace LobbyServer.Manager
{
    public static class AssetManager
    {
        public static ReadOnlyCollection<RealmInfo> RealmInfoStore => new(realmInfoStore);

        private static QueuedCounter<ulong> characterId;

        private static List<VersionInfo> versionInfoStore;
        private static List<RealmInfo> realmInfoStore;
        private static Dictionary<byte, WorldPosition> characterSpawnStore;

        public static void Initialise()
        {
            characterId         = new QueuedCounter<ulong>(DatabaseManager.DataCentre.GetMaxCharacterId());
            versionInfoStore    = new List<VersionInfo>(DatabaseManager.Authentication.LoadVersionInfo());
            realmInfoStore      = new List<RealmInfo>(DatabaseManager.DataCentre.GetRealmList());
            
            characterSpawnStore = DatabaseManager.World.GetCharacterSpawns()
                .ToDictionary(k => k.CityStateId, v => v.Position);
        }

        public static bool IsValidVersion(string file, string version, string digest)
        {
            return uint.TryParse(version, out var uintVersion)
                && versionInfoStore.Exists(vi => vi.File == file && vi.Version == uintVersion && vi.Digest == digest);
        }

        public static RealmInfo GetRealmInfo(ushort id)
        {
            return RealmInfoStore.SingleOrDefault(r => r.Id == id);
        }

        public static ulong GetNewCharacterId()
        {
            return characterId.DequeueValue();
        }
        
        public static bool GetCharacterSpawn(byte cityState, out WorldPosition spawnPosition)
        {
            return characterSpawnStore.TryGetValue(cityState, out spawnPosition);
        }
    }
}
