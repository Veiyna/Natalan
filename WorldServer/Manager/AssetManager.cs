using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Shared.Database;
using Shared.Database.Datacentre;
using Shared.Game;
using WorldServer.Game.ChatChannel;
using WorldServer.Game.ChatChannel.Enums;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Manager
{
    public static class AssetManager
    {
        public static ulong NextItemId => nextItemId++;

        private static ulong nextItemId;
        
        
        public static ulong NextBNpcId => nextBnpcId++;

        private static ulong nextBnpcId;
        
        /// <summary>
        /// Contains player inventory container max capacities.
        /// </summary>
        public static ReadOnlyDictionary<ContainerType, ushort> ContainerCapacities { get; private set; }

        /// <summary>
        /// Contains Armoury container types for items that can be equipped into Armoury containers.
        /// </summary>
        public static ReadOnlyDictionary<ItemUiCategory, ContainerType> EquipArmouryContainerTypes { get; private set; }

        public static ReadOnlyDictionary<CurrencyType, uint> CurrencyToItem { get; private set; } 
        public static ReadOnlyDictionary<uint, CurrencyType> ItemToCurrency { get; private set; } 
        
        public static List<RealmInfo> realmInfoStore;

        public static ulong NoviceNetworkChannel;

        public static void Initialise()
        {
            nextItemId = DatabaseManager.DataCentre.GetNextItemId() + 1ul;
            realmInfoStore      = new List<RealmInfo>(DatabaseManager.DataCentre.GetRealmList());
            InitialiseContainerTypeAttributes();
            InitialiseCurrencyTypes();
            NoviceNetworkChannel = ChatChannelManager.CreateChatChannel(ChatChannelType.NoviceNetworkChat);
        }

        private static void InitialiseCurrencyTypes()
        {
            var currencyTypes = new Dictionary<CurrencyType, uint>();
            currencyTypes.Add(CurrencyType.Gil, 1);
            currencyTypes.Add(CurrencyType.StormSeal, 20);
            currencyTypes.Add(CurrencyType.SerpentSeal, 21);
            currencyTypes.Add(CurrencyType.FlameSeal, 22);
            currencyTypes.Add(CurrencyType.TomestonePhilo, 23);
            currencyTypes.Add(CurrencyType.Mgp, 29);
            CurrencyToItem = new ReadOnlyDictionary<CurrencyType, uint>(currencyTypes);
            ItemToCurrency = new ReadOnlyDictionary<uint, CurrencyType>(currencyTypes.ToDictionary((i) => i.Value, (i) => i.Key));
        }
        private static void InitialiseContainerTypeAttributes()
        {
            var containerCapacities        = new Dictionary<ContainerType, ushort>();
            var equipArmouryContainerTypes = new Dictionary<ItemUiCategory, ContainerType>();
            
            foreach (ContainerType containerType in Enum.GetValues(typeof(ContainerType)))
            {
                if (containerType == ContainerType.None)
                    continue;
                
                MemberInfo member = containerType.GetType().GetMember(containerType.ToString()).FirstOrDefault();
                Debug.Assert(member != null);

                ContainerTypeAttribute attribute = member.GetCustomAttribute<ContainerTypeAttribute>();
                Debug.Assert(attribute != null, "Container type is missing attribute!");

                // cache item ui category to armoury container types 
                if (containerType >= ContainerType.ArmouryOffHand && containerType <= ContainerType.ArmouryMainHand)
                    for (int i = 0; i < attribute.ItemCategories.Count; i++)
                        if (attribute.ItemCategories[i])
                            equipArmouryContainerTypes.Add((ItemUiCategory)i, containerType);

                containerCapacities.Add(containerType, attribute.Capacity);
            }

            ContainerCapacities        = new ReadOnlyDictionary<ContainerType, ushort>(containerCapacities);
            EquipArmouryContainerTypes = new ReadOnlyDictionary<ItemUiCategory, ContainerType>(equipArmouryContainerTypes);
        }
    }
}
