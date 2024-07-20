using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Lumina;
using Lumina.Data.Files;
using Lumina.Data.Parsing.Layer;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;
using Action = Lumina.Excel.GeneratedSheets.Action;

namespace Shared.SqPack
{
    public static class GameTableManager
    {
        public static ExcelSheet<Achievement> Achievements { get; private set; }
        
        public static ExcelSheet<Aetheryte> Aetheryte { get; private set; }
        public static ExcelSheet<ClassJob> ClassJobs { get; private set; }
        public static ExcelSheet<EquipSlotCategory> EquipSlotCategories { get; private set; }
        public static ExcelSheet<Item> Items { get; private set; }
        public static ExcelSheet<ItemUICategory> ItemUiCategories { get; private set; }
        public static ExcelSheet<Quest> Quests { get; private set; }
        public static ExcelSheet<Opening> OpeningEvents { get; private set; }
        public static ExcelSheet<SwitchTalk> SwitchTalk { get; private set; }
        public static ExcelSheet<Warp> Warp { get; private set; }
        public static ExcelSheet<DefaultTalk> DefaultTalk { get; private set; }
        public static ExcelSheet<CustomTalk> CustomTalk { get; private set; }
        public static ExcelSheet<PlaceName> PlaceNames { get; private set; }
        public static ExcelSheet<Race> Races { get; private set; }
        public static ExcelSheet<TerritoryType> TerritoryTypes { get; private set; }
        
        public static ExcelSheet<OnlineStatus> OnlineStatus { get; private set; }
        
        public static ExcelSheet<HowTo> HowTo { get; private set; }
        
        public static ExcelSheet<Companion> Companion { get; private set; }
        public static ExcelSheet<Mount> Mount { get; private set; }
        public static ExcelSheet<CutsceneWorkIndex> CutsceneWorkIndex { get; private set; }
        
        public static ExcelSheet<Emote> Emote { get; private set; }
        
        public static ExcelSheet<Action> Action { get; private set; }
        
        public static ExcelSheet<Tribe> Tribe { get; private set; }
        public static ExcelSheet<ParamGrow> ParamGrow { get; private set; }
        public static ExcelSheet<BaseParam> BaseParam { get; private set; }
        public static ExcelSheet<Status> Status { get; private set; }
        public static ExcelSheet<ChocoboTaxiStand> ChocoboTaxiStand { get; private set; }
        public static ExcelSheet<SpecialShop> SpecialShop{ get; private set; }
        public static ExcelSheet<GilShop> GilShop { get; private set; }
        public static ExcelSheet<ContentFinderCondition> ContentFinderCondition { get; private set; }
        public static ExcelSheet<PreHandler> PreHandler { get; private set; }
        public static ExcelSheet<GCShop> GCShop { get; private set; }
        public static ExcelSheet<Level> Level { get; private set; }
        public static ExcelSheet<QuestChapter> QuestChapter { get; private set; }
        public static ExcelSheet<ScenarioTree> ScenarioTree { get; private set; }
        public static ExcelSheet<QuestBattle> QuestBattle { get; private set; }
        public static ExcelSheet<EventItem> EventItem { get; private set; }
        public static ExcelSheet<InstanceContentGuide> InstanceContentGuide { get; private set; }
        public static ExcelSheet<HousingLandSet> HousingLandSet { get; private set; }
        public static ExcelSheet<GuildleveAssignment> GuildleveAssignment { get; private set; }
        public static ExcelSheet<FittingShopItemSet> FittingShopItemSet { get; private set; }
        public static ExcelSheet<HousingAethernet> HousingAethernet { get; private set; }
        public static ExcelSheet<BNpcBase> BNpcBase { get; private set; }
        public static ExcelSheet<BNpcCustomize> BNpcCustomize { get; private set; }
        public static ExcelSheet<NpcEquip> NpcEquip { get; private set; }
        public static ExcelSheet<ModelChara> ModelChara { get; private set; }
        public static ExcelSheet<ModelSkeleton> ModelSkeleton { get; private set; }
        public static ExcelSheet<GilShopItem> GilShopItem { get; private set; }
        public static ExcelSheet<TopicSelect> TopicSelect { get; private set; }
        public static ExcelSheet<InstanceContent> InstanceContent { get; private set; }
        public static ExcelSheet<PublicContent> PublicContent { get; private set; }

        /// <summary>
        /// Contains AchievementEntry's grouped by CriteriaType.
        /// </summary>
        public static ReadOnlyDictionary<byte /*CriteriaType*/, ReadOnlyCollection<Achievement>> AchievementXCriteriaType { get; private set; }

        /// <summary>
        /// Contains AchievementEntry's grouped by CriteriaCounterType.
        /// </summary>
        public static ReadOnlyDictionary<ushort, ReadOnlyCollection<Achievement>> AchievementXCriteriaCounterType { get; private set; }

        private static Lumina.GameData lumina;
        

        private static Dictionary<ushort, List<LayerCommon.InstanceObject>> InstanceObjects = new();

        private static void Initialise(string assetPath)
        {
            Console.WriteLine("Initialising GameTables...");

            lumina = new GameData(assetPath, new LuminaOptions
            {
                PanicOnSheetChecksumMismatch = false
            });
            
        }

        private static void InitLGB()
        {
            foreach (var row in TerritoryTypes)
            {
                string path = row.Bg;
                if (String.IsNullOrEmpty(path))
                    continue;

                path = "bg/" + path.Remove(path.IndexOf("/level/"));
                
                
                var bg = lumina.GetFile<LgbFile>(path + "/level/bg.lgb");
                var planmap = lumina.GetFile<LgbFile>(path + "/level/planmap.lgb");
                var planevent = lumina.GetFile<LgbFile>(path + "/level/planevent.lgb");
                //var planner = lumina.GetFile<LgbFile>(path + "/level/planner.lgb");
                var lgbs = new[]
                {
                    bg, planmap, planevent
                };
                
                foreach (var lgb in lgbs)
                {
                    if (lgb == null)
                    {
                        continue;
                    }
                    foreach (var layer in lgb.Layers)
                    {
                        foreach (var Instanceobj in layer.InstanceObjects)
                        {

                            LayerEntryType[] entryTypes =
                            {
                                LayerEntryType.PopRange, LayerEntryType.ExitRange, LayerEntryType.EventNPC, LayerEntryType.EventRange, LayerEntryType.EventObject, LayerEntryType.BattleNPC
                            };

                            if (entryTypes.Contains(Instanceobj.AssetType))
                            {
                                var key = (ushort)row.RowId;
                                if(!InstanceObjects.ContainsKey(key)) 
                                    InstanceObjects.Add(key, []);

                                InstanceObjects[key].Add(Instanceobj);
                            }


                        }
                    }
                }
            }
            
            Console.WriteLine($"Cached {InstanceObjects.Values.Count} InstanceObjects");
        }
        
        public static (ushort?, LayerCommon.InstanceObject?) GetLGBEntry(LayerEntryType type, uint id)
        {
            foreach(var innerList in InstanceObjects)
            {
                foreach(var item in innerList.Value)
                {
                    if (item.InstanceId == id && item.AssetType == type)
                        return (innerList.Key, item);
                }
            }
            return (null, null);
        }

        private static ExcelSheet<T> LoadSheet<T>() where T : ExcelRow
        {
            var sheet = lumina.GetExcelSheet<T>();
            return sheet;
        }

        public static void InitialiseLobby(string assetPath)
        {
            Initialise(assetPath);

            var sw = new Stopwatch();
            sw.Start();

            ClassJobs = LoadSheet<ClassJob>();
            Races     = LoadSheet<Race>();
            HowTo      = LoadSheet<HowTo>();
            Companion      = LoadSheet<Companion>();
            Mount      = LoadSheet<Mount>();
            CutsceneWorkIndex      = LoadSheet<CutsceneWorkIndex>();
            Aetheryte = LoadSheet<Aetheryte>();
            Items = LoadSheet<Item>();

            Console.WriteLine($"Initialised GameTables in {sw.ElapsedMilliseconds}ms.");
        }
        
        public static void InitialiseWorld(string assetPath)
        {
            Initialise(assetPath);

            var sw = new Stopwatch();
            sw.Start();

            Achievements        = LoadSheet<Achievement>();
            Aetheryte = LoadSheet<Aetheryte>();
            ClassJobs           = LoadSheet<ClassJob>();
            EquipSlotCategories = LoadSheet<EquipSlotCategory>();
            Items               = LoadSheet<Item>();
            ItemUiCategories    = LoadSheet<ItemUICategory>();
            Quests              = LoadSheet<Quest>();
            Warp = LoadSheet<Warp>();
            OpeningEvents = LoadSheet<Opening>();
            DefaultTalk = LoadSheet<DefaultTalk>();
            SwitchTalk = LoadSheet<SwitchTalk>();
            CustomTalk = LoadSheet<CustomTalk>();
            PlaceNames          = LoadSheet<PlaceName>();
            Races               = LoadSheet<Race>();
            TerritoryTypes      = LoadSheet<TerritoryType>();
            OnlineStatus      = LoadSheet<OnlineStatus>();
            HowTo      = LoadSheet<HowTo>();
            Companion      = LoadSheet<Companion>();
            Mount      = LoadSheet<Mount>();
            CutsceneWorkIndex      = LoadSheet<CutsceneWorkIndex>();
            Emote      = LoadSheet<Emote>();
            Action      = LoadSheet<Action>();
            Tribe      = LoadSheet<Tribe>();
            ParamGrow      = LoadSheet<ParamGrow>();
            BaseParam      = LoadSheet<BaseParam>();
            Status      = LoadSheet<Status>();
            ChocoboTaxiStand      = LoadSheet<ChocoboTaxiStand>();
            GilShop      = LoadSheet<GilShop>();
            ContentFinderCondition      = LoadSheet<ContentFinderCondition>();
            SpecialShop     = LoadSheet<SpecialShop>();
            PreHandler     = LoadSheet<PreHandler>();
            GCShop     = LoadSheet<GCShop>();
            Level    = LoadSheet<Level>();
            QuestChapter    = LoadSheet<QuestChapter>();
            ScenarioTree    = LoadSheet<ScenarioTree>();
            QuestBattle    = LoadSheet<QuestBattle>();
            EventItem    = LoadSheet<EventItem>();
            InstanceContentGuide    = LoadSheet<InstanceContentGuide>();
            HousingLandSet    = LoadSheet<HousingLandSet>();
            GuildleveAssignment    = LoadSheet<GuildleveAssignment>();
            FittingShopItemSet    = LoadSheet<FittingShopItemSet>();
            HousingAethernet    = LoadSheet<HousingAethernet>();
            BNpcBase = LoadSheet<BNpcBase>();
            BNpcCustomize = LoadSheet<BNpcCustomize>();
            NpcEquip = LoadSheet<NpcEquip>();
            ModelChara = LoadSheet<ModelChara>();
            ModelSkeleton = LoadSheet<ModelSkeleton>();
            GilShopItem = LoadSheet<GilShopItem>();
            TopicSelect = LoadSheet<TopicSelect>();
            InstanceContent = LoadSheet<InstanceContent>();
            PublicContent = LoadSheet<PublicContent>();



            InitLGB();

            AchievementXCriteriaType = new ReadOnlyDictionary<byte, ReadOnlyCollection<Achievement>>(Achievements
                .GroupBy(e => e.Type)
                .OrderBy(g => g.Key)
                .ToDictionary(o => o.Key, o => new ReadOnlyCollection<Achievement>(o.ToList())));

            AchievementXCriteriaCounterType = new ReadOnlyDictionary<ushort, ReadOnlyCollection<Achievement>>(Achievements
                .Where(e => e.Type == 1)
                .GroupBy(w => w.Data[0])
                .OrderBy(g => g.Key)
                .ToDictionary(o => (ushort)o.Key, o => new ReadOnlyCollection<Achievement>(o.ToList())));

            Console.WriteLine($"Initialised GameTables in {sw.ElapsedMilliseconds}ms.");
        }
    }
}
