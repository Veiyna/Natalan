using System;
using Shared.Network;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class MiscHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientLogout)]
        public static void HandleClientLogout(WorldSession session, SubPacket subPacket)
        {
            session.Send(new ServerLogout
            {
                Flag1 = 1
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientPing)]
        public static void HandleClientPing(WorldSession session, ClientPing ping)
        {
            session.Send(new ServerPing
            {
                Timestamp = (ulong)(ping.Timestamp + 0x000014D00000000)
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientUnk1, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientUnk1(WorldSession session, SubPacket subPacket)
        {
            session.Send(new ServerUnk1());
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientNoviceNetworkRequestJoin, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientNoviceNetworkRequestJoin(WorldSession session, ClientNoviceNetworkRequestJoin subPacket)
        {
            if(subPacket.Unknown != 0)
                session.Send(new ServerNoviceNetworkId());
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientDye, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientDye(WorldSession session, ClientDye dyeInfo)
        {   
            Console.WriteLine(dyeInfo.ContainerType);
            Console.WriteLine(dyeInfo.Slot);
            Console.WriteLine(dyeInfo.DyeContainer1);
            Console.WriteLine(dyeInfo.DyeContainer2);
            Console.WriteLine(dyeInfo.DyeSlot1);
            Console.WriteLine(dyeInfo.DyeSlot2);
            session.Player.SendStateFlags();
            session.Player.Inventory.DyeItem(dyeInfo.ContainerType, (ushort)dyeInfo.Slot, dyeInfo.DyeContainer1, dyeInfo.DyeSlot1, dyeInfo.DyeContainer2, dyeInfo.DyeSlot2);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientPerformNote, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandlePerformNote(WorldSession session, ClientPerformNote subPacket)
        {
            session.Player.SendMessageToVisible(new ServerPerformNote
            {
                Data = subPacket.Data
            });
        }
    }
}
