﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketClientHandlerId.ClientGmCommandInt)]
public class ClientGmCommandInt : SubPacket
{
    public GmCommand Command { get; private set; }
    public GmCommandParameters Parameters { get; private set; }

    public override void Read(BinaryReader reader)
    {
        Command    = (GmCommand)reader.ReadUInt32();
        Parameters = new GmCommandParameters(reader.ReadBytes(20), reader.ReadUInt32(), "");
        reader.Skip(20u);
    }
}