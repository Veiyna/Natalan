using System;
using Shared.Game.Enum;
using Shared.Network;
using WorldServer.Game.ContentFinder;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ContentFinderHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientContentFinderRequestInfo, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleContentFinderRequestInfo(WorldSession session, ClientContentFinderRequestInfo info)
        {
            #if DEBUG
                Console.WriteLine("Client requested content finder info");
            #endif
            
            // TODO
            session.Send(new ServerContentFinderDutyInfo
            {
                PenaltyTime = 0
            });
            
            session.Send(new ServerContentFinderPlayerInNeed
            {
                InNeed = new ClassJobRole[0x10]
            });
        }
        
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientCFRegisterDuty, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleCFRegister(WorldSession session, ClientCFRegisterDuty info)
        {
            var player = session.Player;
            if (player.Party is not null)
            {
                var party = player.Party;
                ContentFinderManager.RegisterForContent(party.Players.ToArray(), info.Duties, info.UndersizedFlag);
            }
            else
            {
                ContentFinderManager.RegisterForContent([player], info.Duties, info.UndersizedFlag);
            }
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientCFCancel, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleCFCancel(WorldSession session, SubPacket info)
        {
            foreach (var player in session.Player.PartyPlayers)
            {
                ContentFinderManager.Withdraw(player);
            }

        }
        
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientCFCommence, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleCFCommence(WorldSession session, ClientCFCommence info)
        {
            if (info.Param == 1)
            {
                foreach (var player in session.Player.PartyPlayers)
                {
                    ContentFinderManager.Withdraw(player);
                }
            }
            else
            {
                var group = ContentFinderManager.GetPlayerGroup(session.Player);
                ContentFinderManager.BeginContent(group);
            }

        }
    }
}
