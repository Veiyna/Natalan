﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerMessage)]
public class ServerMessage : SubPacket
{
    public byte Flags;
    public string Message;

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Flags);

        if (Flags == 0 || (Flags & 0x01) != 0)
        {
            writer.WriteStringLength(Message, 0x0300);
            writer.Pad(7u);
        }
    }
}