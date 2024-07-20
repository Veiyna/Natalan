using System.Collections.Generic;
using System.IO;
using Shared.Network;
using WorldServer.Game.Entity;
using WorldServer.Game.Social;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerParty)]
    public class ServerParty : SubPacket
    {
        public class Member
        {
            public ulong Id { get; }
            public string Name { get; }
            public uint ActorId { get; }
            public byte ClassJobId { get; }
            public ushort Level { get; }
            public uint Hp { get; }
            public uint MaxHp { get; }
            public ushort Mp { get; }
            public ushort MaxMp { get; }
            public ushort Tp { get; }
            public ushort TerritoryId { get; }

            public Member(Player player)
            {
                Id          = player.Character.Id;
                Name        = player.Character.Name;
                ActorId     = player.Character.ActorId;
                ClassJobId  = player.Character.ClassJobId;
                Level       = player.Character.GetClassInfo(player.Character.ClassJobId).Level;
                TerritoryId = (ushort)player.Map.Entry.RowId;
                Hp          = player.HP;
                MaxHp       = player.MaxHP;
                Mp          = (ushort)player.MP;
                MaxMp       = (ushort)player.MaxMP;
                Tp          = 10000;
            }

            public Member(Party.Member member)
            {
                Id   = member.Id;
                Name = member.Name;
            }
        }

        public byte LeaderIndex;
        public List<Member> PartyMembers = new List<Member>();
        public Party Party;

        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < Party.FullPartySize; i++)
            {
                if (i < PartyMembers.Count)
                {
                    Member member = PartyMembers[i];
                    writer.WriteStringLength(member.Name, 0x20);
                    writer.Pad(8u);
                    if (Version.Version.StartsWith('7'))
                    {
                        writer.Write((ulong)10);
                    }
                    writer.Write(member.Id);
                    writer.Write(member.ActorId);

                    
                    writer.Write((uint)0);
                    writer.Write((uint)0);
                    writer.Write(member.Hp);
                    writer.Write(member.MaxHp);
                    writer.Write(member.Mp);
                    writer.Write(member.MaxMp);
                    writer.Write(member.Tp);
                    writer.Write(member.TerritoryId);
                    writer.Write((byte)1); // gpose allowed
                    writer.Write(member.ClassJobId);
                    writer.Write((byte)0);
                    writer.Write((byte)member.Level);
                    writer.Write((byte)0);
                    
                    writer.Pad(7u);
                    
                    writer.Pad(360u);
                }
                else
                {
                    if (Version.Version.StartsWith('7'))
                    {
                        writer.Pad(8);
                    }
                    writer.Pad(448u);
                    
                }
                    
            }

            writer.Pad(8u);
            writer.Write(Party?.ChatChannel ?? 0ul);
            writer.Write(LeaderIndex);
            writer.Write((byte)PartyMembers.Count);
            writer.Pad(6u);
        }
    }
}
