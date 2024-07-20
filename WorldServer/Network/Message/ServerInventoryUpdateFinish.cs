﻿using System.IO;
using Shared.Network;

namespace WorldServer.Network.Message
{
    [SubPacket(SubPacketServerHandlerId.ServerInventoryUpdateFinish)]
    public class ServerInventoryUpdateFinish : SubPacket
    {
        public uint Id;
        
        public override void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Id);
            writer.Pad(8u);
        }
    }
}
