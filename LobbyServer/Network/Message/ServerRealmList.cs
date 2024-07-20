﻿using System.Collections.Generic;
using System.IO;
using Shared.Network;

namespace LobbyServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerRealmList)]
    public class ServerRealmList : SubPacket
    {
        public const byte MaxRealmsPerPacket = 6;

        public struct RealmInfo
        {
            public ushort Id;
            public ushort Position;
            public uint Flags;      // 0x02 = World not accepting new characters
            public uint Icon;       // 2 = bonus XP star
            public string Name;
        }

        public ulong Sequence;
        public ushort Final;
        public ushort Offset;

        public List<RealmInfo> Realms = new();

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Sequence);
            writer.Write(Final);
            writer.Write(Offset);    
            writer.Write(Realms.Count);
            writer.Pad(8);

            for (var i = 0; i < MaxRealmsPerPacket; i++)
            {
                if (i < Realms.Count)
                {
                    var realm = Realms[i];
                    writer.Write(realm.Id);
                    writer.Write(realm.Position);
                    writer.Write(realm.Flags);
                    writer.Pad(4);
                    writer.Write(realm.Icon);
                    writer.Pad(4);
                    writer.WriteStringLength(realm.Name, 0x40);
                }
                else
                    writer.Pad(0x54);
            }
        }
    }
}
