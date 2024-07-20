using System.Collections;
using System.Collections.Generic;
using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerSocialList)]
    public class ServerSocialList : SubPacket
    {
        public class Member
        {
            public ulong Id { get; }
            public string Name { get; }
            public ushort TerritoryId { get; }
            public byte Language { get; } = 1;
            public uint LanguageMask { get; } = 0x02u;
            public BitArray OnlineStatusMask { get; set; } = new BitArray(8 * 8);
            public byte GrandCompany { get; }
            public byte ClassJobId { get; }
            public ushort Level { get; }
            
            public byte isLeader { get; set; }

            public byte HasComment { get; set; }
            
            public string FCTag { get; set; }

            public Member(Player player)
            {
                Id           = player.Character.Id;
                Name         = player.Character.Name;
                TerritoryId  = (ushort)player.Map.Entry.RowId;
                ClassJobId   = player.Character.ClassJobId;
                Level        = player.Character.GetClassInfo(player.Character.ClassJobId).Level;
                FCTag = player.FreeCompany?.Tag ?? "";
                GrandCompany = player.Character.GrandCompany;
                
                LanguageMask = player.Character.SocialInfo.SelectRegion;
                OnlineStatusMask = player.OnlineMask;

                if (player.Character.SocialInfo.SearchComment.Length > 0)
                    HasComment = 1;

            }

            public Member(Party.Member member)
            {
                Id   = member.Id;
                Name = member.Name;
                FCTag = "";
            }
        }

        public SocialListType ListType;
        public ushort Sequence;
        public List<Member> SocialMembers = new List<Member>();

        public override void Write(BinaryWriter writer)
        {
            writer.Pad(12u);
            writer.Write((byte)ListType);
            writer.Write(Sequence);
            writer.Pad(1u);

            for (int i = 0; i < Party.FullPartySize; i++)
            {
                if (i < SocialMembers.Count)
                {
                    Member member = SocialMembers[i];
                    writer.Write(member.Id);
                    writer.Write((byte)0);

                        
                    writer.Write(member.TerritoryId);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write(member.isLeader);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    if (Version.Version.StartsWith("7"))
                    {
                        writer.Write((ulong)1);
                    }
                    writer.Write((byte)0);
                    writer.Write((byte)0);

                    writer.Write((byte)0); // friend group
                    writer.Write((byte)0x10); // allows right click menu?
                    writer.Write(member.TerritoryId);
                    writer.Write((ushort)0);
                    writer.Write(member.GrandCompany);
                    writer.Write(member.Language);
                    writer.Write((byte)member.LanguageMask);
                    writer.Write(member.HasComment);
                    writer.Pad(4u);
                    writer.Write(member.OnlineStatusMask.ToArray());
                    writer.Write(member.ClassJobId);
                    writer.Write((byte)0);
                    writer.Write(member.Level);
                    writer.Write((ushort)0);
                    var flags = SocialListDisplayFlags.Unknown;
                    writer.Write((byte)flags);            // enables disband button (flags?) 0, 1, 2, 3 when joined to novice network, 9, 10, 25?| controls novice network icon, wanderer, traveler status,
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.WriteStringLength(member.Name, 0x20);
                    writer.WriteStringLength(member.FCTag, 12);
                }
                else
                    writer.Pad(104u);
            }

            writer.Pad(208);
        }
    }
}
