using System.Collections.Generic;
using System.IO;
using Shared.Database.Datacentre;
using Shared.Game.Enum;
using Shared.Network;

namespace LobbyServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerCharacterList)]
    public class ServerCharacterList : SubPacket
    {
        public const byte MaxCharactersPerPacket = 2;

        public byte Offset;
        public byte VeteranRank;
        public uint DaysSubscribed;
        public uint SubscriptionDaysRemaining;
        public uint DaysTillNextVeteranRank;
        public ushort RealmCharacterLimit;
        public ushort AccountCharacterLimit;
        public Expansion Expansion;

        public List<(uint index, string RealmName, string RealmName2, CharacterInfo character)> Characters = new(2);

        public override void Write(BinaryWriter writer)
        {
            writer.Write(0ul);
            writer.Write(Offset);
            writer.Write(MaxCharactersPerPacket);
            writer.Write((ushort)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0x10); // flags of some sort (0x10 = hides subscription days remaining, 0x80 = legacy)
            writer.Pad(26u);
            //writer.Pad(1u);
            writer.Write((byte)0);   
            writer.Write((byte)0);
            writer.Write((byte)0); // 0x80 = hides remaining playtime???
            writer.Write(VeteranRank);
            writer.Write((ushort)0);
            writer.Write(DaysSubscribed);
            writer.Write(SubscriptionDaysRemaining);
            writer.Write(DaysTillNextVeteranRank);
            writer.Write(RealmCharacterLimit);
            writer.Write(AccountCharacterLimit);
            writer.Write((uint)Expansion);
            writer.Write((uint) 0 );
            writer.Write((uint) 0 );
            writer.Write((uint) 0 );
            
            

            for (var i = 0; i < MaxCharactersPerPacket; i++)
            {
                if (i < Characters.Count)
                {
                    var characterInfo = Characters[i].character;
                    writer.Write((uint)0);                   // some character id, persistent even after removing character
                    writer.Write((uint)0); 
                    writer.Write(characterInfo.Id);
                    
                    writer.Write(Characters[i].index);
                    
                    
                    writer.Write((uint)0x02);           // flags (0x01 = invalid account, 0x02 = character rename, 0x08 = legacy)
                    writer.Write(characterInfo.CurrentRealmId);
                    writer.Write(characterInfo.RealmId);
                    
                    writer.Pad(16u);
                    
                    writer.WriteStringLength(characterInfo.Name, 0x20);
                    writer.WriteStringLength(Characters[i].RealmName2, 0x20);
                    writer.WriteStringLength(Characters[i].RealmName, 0x20);
                    writer.WriteStringLength(characterInfo.BuildJsonData(), 1024);
                    writer.Pad(20u);
                }
                else
                    writer.Pad(1184);
            }
        }
    }
}
