using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotRecast.Detour;
using DotRecast.Detour.Io;
using Newtonsoft.Json;
using WorldServer.Game.Map;

namespace WorldServer.Data
{
    public static class DataManager
    {
        
        public static Dictionary<uint, ActionData> Actions = new();
        public static Dictionary<uint, Coffer> Coffers = new();
        public static Dictionary<uint, StatusEffectData> StatusEffectDatas = new();
        public static Dictionary<uint, BNpcTemplate> BNpcTemplates = new();
        public static List<VersionData> VersionData = [];
        public static FileSystemWatcher watcher = new();
        public static ConcurrentDictionary<string, DtNavMesh> NavMeshes = new();
        public static void Initialise()
        {

            watcher.Path = "Data";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += OnChanged;
            LoadActions();
            LoadCoffers();
            LoadStatusEffects();
            LoadVersions();
            LoadBNpcTemplates();
            LoadNavMesh();

        }

        
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);
            Console.WriteLine("Static data modified. Reloading.");
            LoadActions();
            LoadStatusEffects();
            LoadVersions();
            LoadBNpcTemplates();
            foreach (var player in MapManager.GetPlayers())
            {
                player.Session.Version = VersionData.FirstOrDefault(v => v.Version == player.Session.Version.Version);
            }
        }
        
        public static void LoadBNpcTemplates()
        {
            BNpcTemplates.Clear();
            foreach (var filename in Directory.EnumerateFiles(@"Data\BNpcTemplates", "*.json", SearchOption.AllDirectories))
            {
                var data = File.ReadAllText(filename);
                BNpcTemplate[] obj = [];
                try
                {
                    obj = JsonConvert.DeserializeObject<BNpcTemplate[]>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
                

                foreach (var action in obj)
                {
                    BNpcTemplates[action.instanceId] = action;
                }
                
            }
            Console.WriteLine($"Loaded {BNpcTemplates.Count} BNpc entries");
        }
        
        public static void LoadCoffers()
        {
            Coffers.Clear();
            foreach (var filename in Directory.EnumerateFiles(@"Data\Coffers", "*.json", SearchOption.AllDirectories))
            {
                var data = File.ReadAllText(filename);
                Coffer[] obj = [];
                try
                {
                    obj = JsonConvert.DeserializeObject<Coffer[]>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
                

                foreach (var action in obj)
                {
                    Coffers[action.ItemId] = action;
                }
                
            }
            Console.WriteLine($"Loaded {Coffers.Count} item coffers");
        }
        public static void LoadActions()
        {
            Actions.Clear();
            foreach (var filename in Directory.EnumerateFiles(@"Data\Actions", "*.json", SearchOption.AllDirectories))
            {
                var data = File.ReadAllText(filename);
                ActionData[] obj = [];
                try
                {
                    obj = JsonConvert.DeserializeObject<ActionData[]>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
                

                foreach (var action in obj)
                {
                    Actions[action.Id] = action;
                }
                
            }
            Console.WriteLine($"Loaded {Actions.Count} action entries");
        }
        
        public static void LoadStatusEffects()
        {
            StatusEffectDatas.Clear();
            foreach (var filename in Directory.EnumerateFiles(@"Data\StatusEffect", "*.json", SearchOption.AllDirectories))
            {
                var data = File.ReadAllText(filename);
                StatusEffectData[] obj = [];
                try
                {
                    obj = JsonConvert.DeserializeObject<StatusEffectData[]>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
                

                foreach (var effect in obj)
                {
                    StatusEffectDatas[effect.Id] = effect;
                }
                
            }
            Console.WriteLine($"Loaded {StatusEffectDatas.Count} status effect entries");
        }
        
        public static void LoadVersions()
        {
            VersionData.Clear();
            foreach (var filename in Directory.EnumerateFiles(@"Data\Version", "*.json5"))
            {
                var data = File.ReadAllText(filename);
                try
                {
                    VersionData obj = JsonConvert.DeserializeObject<VersionData>(data);
                    VersionData.Add(obj);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
                
                

            }
            Console.WriteLine($"Loaded {VersionData.Count} version entries");
        }
        
        public static void LoadNavMesh()
        {
            NavMeshes.Clear();
            Parallel.ForEach(Directory.EnumerateFiles(@"Data\Navmesh", "*.nav", SearchOption.AllDirectories), filename =>
            {
                var data = File.ReadAllBytes(filename);
                try
                {
                    var reader = new DtMeshSetReader();
                    using var ms = new MemoryStream(data);
                    using var br = new BinaryReader(ms);
                    DtNavMesh mesh = reader.Read32Bit(br, 6);
                    NavMeshes.TryAdd(Path.GetFileNameWithoutExtension(filename), mesh);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load {filename}");
                }
            });
            Console.WriteLine($"Loaded {NavMeshes.Count} navmesh entries");
        }
        
        public static ActionData GetAction(uint actionId)
        {
            return Actions.GetValueOrDefault(actionId);

        }
        
        public static Coffer GetCoffer(uint itemId)
        {
            return Coffers.GetValueOrDefault(itemId);
        }
        
        public static StatusEffectData GetStatusEffect(uint statuseffectId)
        {
            return StatusEffectDatas.GetValueOrDefault(statuseffectId);

        }
        
        public static VersionData GetVersion(string version)
        {
            return VersionData.FirstOrDefault(v => v.FFXIVModule == version);
        }
        
        public static DtNavMesh GetNavMesh(string name)
        {
            return NavMeshes.GetValueOrDefault(name);

        }
        
        public static BNpcTemplate GetBNpcTemplate(uint name)
        {
            return BNpcTemplates.Values.FirstOrDefault(b => b.instanceId == name);
        }
    }
}