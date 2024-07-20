using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Map.Enums;
using TerritoryType = Lumina.Excel.GeneratedSheets.TerritoryType;

namespace WorldServer.Game.Map
{
    public static class MapManager 
    {
        private static readonly Dictionary<(uint,uint), Territory> TerritoryInstances = new();
        
        private static readonly List<Player> players = new();
        private static readonly ReaderWriterLockSlim playerMutex = new();

        public static void Initialise()
        {
            var sw = new Stopwatch();
            sw.Start();

            foreach (TerritoryType entry in GameTableManager.TerritoryTypes)
            {
                if (entry.Name == string.Empty)
                    continue;

                if (entry.TerritoryIntendedUse == (int)TerritoryIntendedUse.HousingArea)
                    continue;
                
                CreateInstance(entry.RowId);
                
            }
            
            Console.WriteLine($"Initialised {TerritoryInstances.Count} map(s) in {sw.ElapsedMilliseconds}ms.");
        }
        
        public static void AddToMap(Actor actor)
        {
            if (actor.Position == null)
                return;

            if (TerritoryInstances.TryGetValue((actor.Position.TerritoryId,actor.Position.InstanceId), out Territory instancedTerritory))
                instancedTerritory.AddActor(actor);
        }

        public static uint RegisterTerritory(Territory territory)
        {
            var territoryId = territory.Entry.RowId;   
            var instanceId = GetNextInstanceIdForTerritory(territoryId);
            TerritoryInstances.Add((territoryId, instanceId),territory);
            territory.InstanceId = instanceId;
            territory.OnInstanceRegister();
            return instanceId;
        }
        
        public static bool CheckInstance(uint territoryId, uint instanceId)
        {
            return TerritoryInstances.ContainsKey((territoryId, instanceId));
        }

        public static Territory CreateInstance(uint territoryId)
        {
            var entry = GameTableManager.TerritoryTypes.GetRow(territoryId);
            Territory territory;
            switch ((TerritoryIntendedUse)entry.TerritoryIntendedUse)
            {
                case TerritoryIntendedUse.Eureka:
                    territory = new PublicContent(entry);
                    break;
                case TerritoryIntendedUse.Dungeon:
                case TerritoryIntendedUse.AllianceRaid:
                case TerritoryIntendedUse.Trial:
                    territory = new InstanceContent(entry);
                    break;
                default:
                    territory = new Territory(entry);
                    break;
            }
            
            RegisterTerritory(territory);
            return territory;
        }

        public static uint GetNextInstanceIdForTerritory(uint territoryId)
        {
            return (uint)TerritoryInstances.Count(t => t.Value.Entry.RowId == territoryId);
        }
        

        public static void _AddPlayer(Player player)
        {
            try
            {
                playerMutex.EnterWriteLock();
                players.Add(player);
            }
            finally
            {
                playerMutex.ExitWriteLock();
            }
        }

        public static void _RemovePlayer(Player player)
        {
            try
            {
                playerMutex.EnterWriteLock();
                players.Remove(player);
            }
            finally
            {
                playerMutex.ExitWriteLock();
            }
        }

        /// <summary>
        /// Find a player in the world (any territory).
        /// </summary>
        public static Player FindPlayer(string name)
        {
            try
            {
                playerMutex.EnterReadLock();
                return players.SingleOrDefault(p => p.Character.Name == name);
            }
            finally
            {
                playerMutex.ExitReadLock();
            }
        }

        /// <summary>
        /// Find a player in the world (any territory).
        /// </summary>
        public static Player FindPlayer(ulong characterId)
        {
            try
            {
                playerMutex.EnterReadLock();
                return players.SingleOrDefault(p => p.Character.Id == characterId);
            }
            finally
            {
                playerMutex.ExitReadLock();
            }
        }
        
        public static Player FindPlayer(uint actorId)
        {
            try
            {
                playerMutex.EnterReadLock();
                return players.SingleOrDefault(p => p.Character.ActorId == actorId);
            }
            finally
            {
                playerMutex.ExitReadLock();
            }
        }
        
        public static List<Player> GetPlayers()
        {
            return players;
        }
        
        public static List<Player> GetFreeCompanyPlayers(ulong id)
        {
            return players.Where(p => p.FreeCompany.Id == id).ToList();
        }
        
        

        public static void Update(double lastTick, long tickCount)
        {
            // TODO: change this to a multithreaded implementation
            foreach (KeyValuePair<(uint,uint), Territory> pair in TerritoryInstances.ToList())
                pair.Value.Update(lastTick, tickCount);
            
            /*
            var territoryInstancesSnapshot = TerritoryInstances.ToList();
            Parallel.ForEach(territoryInstancesSnapshot, pair =>
            {
                pair.Value.Update(lastTick, tickCount);
            });
            */
        }
    }
}
