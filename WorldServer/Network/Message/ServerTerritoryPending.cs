﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerTerritoryPending)]
    public class ServerTerritoryPending : SubPacket
    {
        public uint LogMessageId;
        public ushort TerritoryId;

        public override void Write(BinaryWriter writer)
        {
            writer.Write(LogMessageId);
            writer.Write(TerritoryId);
            writer.Write((ushort)0);
            writer.Write((ushort)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((ushort)0);
        }
    }
}
