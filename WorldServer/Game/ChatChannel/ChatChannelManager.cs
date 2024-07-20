using System;
using System.Collections.Generic;
using System.Linq;
using WorldServer.Game.ChatChannel.Enums;
using WorldServer.Game.Entity;
using WorldServer.Game.Map;
using WorldServer.Manager;
using WorldServer.Network.Message;

namespace WorldServer.Game.ChatChannel;

public static class ChatChannelManager
{
    private static Dictionary<ulong, ChatChannel> ChatChannels { get; set; } = new();
    
    private static uint NextChannelId => ++nextChannelId;
    private static uint nextChannelId;
    
    
    public static ulong CreateChatChannel(ChatChannelType type)
    {
        var ChatChannel = new ChatChannel();
        var ChatChannelId = new ChatChannelId
        {
            IncrementalId = NextChannelId,
            ChatType = (ushort)type,
            WorldId = AssetManager.realmInfoStore.First().Id
        };
        ChatChannels.Add(ChatChannelId.Id, ChatChannel);

        return ChatChannelId.Id;
    }

    public static void AddToChannel(ulong channelId, Player player)
    {
        if (!IsChannelValid(channelId))
        {
            Console.WriteLine($"Failed to add player to chat channel, channel {channelId} doesn't exist. ");
            return;
        }
        var playerId = player.Character.ActorId;
        
        ChatChannels[channelId].MemberIds.Add(playerId);
        
    }
    
    public static void RemoveFromChannel(ulong channelId, Player player)
    {
        if (!IsChannelValid(channelId))
        {
            Console.WriteLine($"Failed to remove player to chat channel, channel {channelId} doesn't exist. ");
            return;
        }

        if (player == null)
            return;
        
        var playerId = player.Character.ActorId;
        
        ChatChannels[channelId].MemberIds.Remove(playerId);
        
    }

    public static void SendMessageToChannel(ulong channelId, Player sender, string message)
    {
        if (!IsChannelValid(channelId))
        {
            Console.WriteLine($"Failed to send message player to chat channel, channel {channelId} doesn't exist. ");
            return;
        }

        var channel = ChatChannels[channelId];

        foreach (var id in channel.MemberIds)
        {
            if (id == sender.Character.ActorId)
                continue;

            var player = MapManager.FindPlayer(id);
            
            player?.ChatSession.Send(new ServerChannelChat
            {
                Sender = sender,
                Message = message,
                ChannelId = channelId
            });
            
        }
    }

    public static bool IsChannelValid(ulong channelId)
    {
        return ChatChannels.ContainsKey(channelId);
    }
}