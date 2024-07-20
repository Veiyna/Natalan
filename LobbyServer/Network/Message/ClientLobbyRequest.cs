﻿using System.IO;
using Shared.Network;

namespace LobbyServer.Network.Message
{
    [SubPacket(SubPacketClientHandlerId.ClientLobbyRequest)]
    public class ClientLobbyRequest : SubPacket
    {
        public ulong Sequence { get; private set; }
        public string Token { get; private set; }
        public string Version { get; private set; }

        public override void Read(BinaryReader reader)
        {
            Sequence = reader.ReadUInt64();
            reader.ReadUInt64();
            reader.Skip(2u);
            Token    = reader.ReadStringLength(0x40);
            Version  = reader.ReadStringLength(0x420);
        }
    }
}
