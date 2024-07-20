﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientEventTerritory)]
public class ClientEventTerritory : SubPacket
{
    public uint EventId { get; private set; }

    public override void Read(BinaryReader reader)
    {
        EventId = reader.ReadUInt32();
    }
}