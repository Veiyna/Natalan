using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MongoDB.Driver;
using Shared.Game;

namespace Shared.Database.World
{
    public class WorldDatabase : Database
    {
        public IEnumerable<(byte CityStateId, WorldPosition Position)> GetCharacterSpawns()
        {
            var spawns = Data.GetCollection<CharacterSpawn>("SpawnPoints").AsQueryable().ToList();
            List<(byte City, WorldPosition Position)> data = [];
            foreach (var spawnpoint in spawns)
            {
                data.Add((spawnpoint.cityStateId, new WorldPosition(spawnpoint.territoryId, new Vector3(spawnpoint.x,spawnpoint.y,spawnpoint.z), spawnpoint.o)));
            }

            return data;
        }
    }
}
