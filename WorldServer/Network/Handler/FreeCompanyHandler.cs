using Shared.Network;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class FreeCompanyHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanyInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanyInfo(WorldSession session, SubPacket packet)
        {
            session.Player.SendFreeCompany();
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanyDialog, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanyDialog(WorldSession session, SubPacket packet)
        {
            session.Send(new ServerFreeCompanyDialog
            {
                FreeCompany = session.Player.FreeCompany
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanySlogan, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanySlogan(WorldSession session, SubPacket packet)
        {
            session.Send(new ServerFreeCompanySlogan
            {
                FreeCompany = session.Player.FreeCompany
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanyParam, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanyParam(WorldSession session, SubPacket packet)
        {
            session.Send(new ServerFreeCompanyParam
            {
                FreeCompany = session.Player.FreeCompany
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanyBoard, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanyBoard(WorldSession session, SubPacket packet)
        {
            session.Send(new ServerFreeCompanyBoard
            {
                FreeCompany = session.Player.FreeCompany
            });
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientFreeCompanyActivity, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleFreeCompanyActivity(WorldSession session, SubPacket packet)
        {
            session.Send(new ServerFreeCompanyActivity
            {
                FreeCompany = session.Player.FreeCompany
            });
        }
        
        
    }
}