﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerNewWorld)]
public class ServerNewWorld : SubPacket
{
    public uint ActorId;

    public override void Write(BinaryWriter writer)
    {
            writer.Write(0ul);
            writer.Write(ActorId);
            writer.Write(0u);
        }
}