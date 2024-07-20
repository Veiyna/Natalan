using System;
using Shared.Command;
using Shared.Network;
using WorldServer.Game.ChatChannel;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Map;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ChatHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientChat, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleChat(WorldSession session, ClientChat chat)
        {
            if (chat.Message.StartsWith(".", StringComparison.Ordinal))
            {
                CommandManager.ParseCommand(chat.Message.Remove(0, 1), out string command, out string[] parameters);
                if (CommandManager.GetCommand(session, command, parameters, out CommandManager.CommandHandler handler))
                {
                        handler.Invoke(session, parameters);
                }
                    
            }
            else
            {
                uint range;
                switch ((ChatType)chat.Type)
                {
                    case ChatType.Say:
                        range = 25;
                        break;
                    case ChatType.Yell:
                        range = 50;
                        break;
                    case ChatType.Shout:
                        range = 100;
                        break;
                    default:
                        range = 25;
                        break;
                        
                }
                
                session.Player.SendMessageToRange(new ServerChat
                {
                    AccountId = session.Player.Character.AccountId,
                    Id = session.Player.Character.Id,
                    Name = session.Player.Character.Name,
                    Type = chat.Type,
                    Message = chat.Message,
                    
                }, range);
            }
        }
        
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientChannelChat)]
        public static void HandleChannelChat(ChatSession session, ClientChannelChat info)
        {
            ChatChannelManager.SendMessageToChannel(info.ChannelId, session.Player, info.Message);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientChannelJoin, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientChannelJoin(WorldSession session, ClientChannelJoin channelJoin)
        {
            ChatChannelManager.AddToChannel(channelJoin.ChannelId, session.Player);
        }
        
        [SubPacketHandler(SubPacketClientHandlerId.ClientTell)]
        public static void HandleTell(ChatSession session, ClientTell info)
        {
            var target = MapManager.FindPlayer(info.TargetName);
            if (target is null)
            {
                session.Send(new ServerTellNotFound
                {
                    RecipientName = info.TargetName
                });
                return;
            }
            
            target.ChatSession.Send(new ServerTell
            {
                ReceipientName = session.Player.Character.Name,
                Message = info.Message,
                ContentId = session.Player.Character.Id,
                WorldId = session.Player.Character.RealmId,
            });
            
        }
    }
}
