using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Game.ContentFinder
{
    public static class ContentFinderManager
    {
        
        private static List<ContentGroup> groups = [];

        public static ContentGroup CreateGroup(uint contentId)
        {
            var group = new ContentGroup(contentId);
            groups.Add(group);

            return group;
        }

        public static void RemoveGroup(ContentGroup group)
        {
            groups.Remove(group);
        }

        public static ContentGroup GetPlayerGroup(Player player)
        {
            foreach (var group in groups)
            {
                foreach (var p in group.Players)
                {
                    if (p == player)
                        return group;
                }
            }

            return null;
        }
        public static ContentGroup GetGroupForContent(uint contentId, Player[] players)
        {
            var availablegroups = groups.Where(c => c.ContentID == contentId);

            foreach (var group in availablegroups)
            {
                if (group.CanTakePlayers(players))
                    return group;
            }

            return CreateGroup(contentId);
        }
        public static void AddToGroup(uint contentId, Player[] players, bool undersizedgroup)
        {
            if (undersizedgroup)
            {
                var undersized = CreateGroup(contentId);
                undersized.Add(players);
                MatchingComplete(undersized);
                return;
            }
                
            var group = GetGroupForContent(contentId, players);

            group.Add(players);
            if (group.Check())
            {
                MatchingComplete(group);
            }
            

        }


        public static void MatchingComplete(ContentGroup group)
        {
            foreach (var player in group.Players)
            {
                player.Session.Send(new ServerContentFinderNotify
                {
                    State = 3,
                    ContentId = (ushort)group.ContentID,
                    ContentGroup = group,
                });
            }
        }

        public static void BeginContent(ContentGroup group)
        {
            var content = group.Condition;
            var instanceContent = MapManager.CreateInstance(content.TerritoryType.Row);
            foreach (var player in group.Players)
            {
                player.TeleportTo(new WorldPosition((ushort)instanceContent.Entry.RowId, Vector3.Zero, 0, instanceContent.InstanceId));
            }
        }

        public static void RegisterForContent(Player[] players, ushort[] duties, byte UndersizedFlag)
        {
            foreach (var player in players)
            {
                player.Session.Send(new ServerCFRegister
                {
                    Player = player,
                    Duties = duties
                });
                player.Session.Send(new ServerSystemLogMessage
                {
                    Id = 897
                });
            
                player.AddOnlineStatus(OnlineStatus.WaitingforDutyFinder);
                bool UndersizedGroup = UndersizedFlag == 0x20;
                
                var duty = GameTableManager.ContentFinderCondition.GetRow(duties[0]);
                AddToGroup(duties[0], players, UndersizedGroup);
            }
        }


        public static void Withdraw(Player player)
        {
            var groups = ContentFinderManager.groups.Where(c => c.Players.Contains(player));
            var contentGroups = groups.ToList();
            foreach (var group in contentGroups)
            {
                group.Remove(player);
            }

            if (!contentGroups.Any())
                return;
            
            player.RemoveOnlineStatus(OnlineStatus.WaitingforDutyFinder);


            player.Session.Send(new ServerCFCancel());
        }
    }
}