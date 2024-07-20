using System.Collections.Generic;
using System.Linq;
using Lumina.Excel.GeneratedSheets;
using Shared.SqPack;
using WorldServer.Game.ContentFinder.Enum;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network.Message;

namespace WorldServer.Game.ContentFinder
{
    public class ContentGroup
    {
        public uint ContentID;
        public ContentFinderCondition Condition;
        public byte RequiredDPS;
        public byte RequiredHealer;
        public byte RequiredTank;
        public List<Player> DPS => this.Players.Where(p => p.Role == Role.MeleeDPS || p.Role == Role.RangedDPS).ToList();
        public List<Player> Healer => this.Players.Where(p => p.Role == Role.Healer).ToList();
        public List<Player> Tank => this.Players.Where(p => p.Role == Role.Tank).ToList();
        public List<Player> Players = new();
        public ContentGroupState State = ContentGroupState.MatchingInProgress;
        public ContentGroup(uint contentId)
        {
            this.ContentID = contentId; 
            Condition = GameTableManager.ContentFinderCondition.GetRow(contentId);
            var MemberType = this.Condition.ContentMemberType.Value;
            this.RequiredTank = MemberType.TanksPerParty;
            this.RequiredHealer = MemberType.HealersPerParty;
            this.RequiredDPS = (byte)(MemberType.MeleesPerParty + MemberType.RangedPerParty);
        }

        public bool Check()
        {
            if (DPS.Count == this.RequiredDPS && Healer.Count == this.RequiredHealer && this.Tank.Count == this.RequiredTank)
            {
                return true;
            }

            return false;
        }

        public void Add(Player player)
        {
            this.Players.Add(player);
            SendUpdate();
        }
        
        public void Add(Player[] players)
        {
            foreach (var player in players)
            {
                this.Players.Add(player);
            }
            SendUpdate();
        }

        public void Remove(Player player)
        {
            this.Players.Remove(player);
            if(this.Players.Count == 0)
                ContentFinderManager.RemoveGroup(this);
            SendUpdate();
        }

        public bool CanTakePlayers(Player[] players)
        {
            var dpscount = 0;
            var tankcount = 0;
            var healercount = 0;
            foreach (var player in players)
            {
                if (player.Role == Role.Healer)
                    healercount++;

                if (player.Role == Role.Tank)
                    tankcount++;

                if (player.Role == Role.MeleeDPS || player.Role == Role.RangedDPS)
                    dpscount++;
            }

            var freeDPS = this.RequiredDPS - DPS.Count;
            var freeHealer = this.RequiredHealer - Healer.Count;
            var freeTank = this.RequiredTank - Tank.Count;
            if (dpscount > freeDPS || healercount > freeHealer || tankcount > freeTank)
                return false;

            return true;

        }
        public bool HasSpaceForRole(Role role)
        {
            if (role == Role.Tank)
            {
                if (this.Tank.Count < this.RequiredTank)
                    return true;
            }
            if (role == Role.MeleeDPS || role == Role.RangedDPS)
            {
                if (this.DPS.Count < this.RequiredDPS)
                    return true;
            }
            
            if (role == Role.Healer)
            {
                if (this.Healer.Count < this.RequiredHealer)
                    return true;
            }

            return false;
        }
        public void SendUpdate()
        {
            foreach (var player in Players)
            {
                player.Session.Send(new ServerContentFinderMemberStatus
                {
                    ContentId = (ushort)this.ContentID,
                    CurrentTank = (byte)this.Tank.Count,
                    CurrentHealer = (byte)this.Healer.Count,
                    CurrentDPS = (byte)this.DPS.Count,
                    MaxTank = this.RequiredTank,
                    MaxHealer = this.RequiredHealer,
                    MaxDPS = this.RequiredDPS,
                    Status = 2,
                });
            }

        }
    }
}