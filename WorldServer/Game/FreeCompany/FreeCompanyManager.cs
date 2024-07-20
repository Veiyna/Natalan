using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Shared.Database;
using Shared.Database.Datacentre;
using WorldServer.Game.ChatChannel;
using WorldServer.Game.ChatChannel.Enums;
using WorldServer.Game.Entity;
using WorldServer.Network.Message;

namespace WorldServer.Game.FreeCompany;

using FreeCompany = Shared.Database.Datacentre.Models.FreeCompany;
public static class FreeCompanyManager
{

    private static List<FreeCompany> FreeCompanies { get; set; }

    private static ulong NextFreeCompanyId => FreeCompanies.Select(r => r.Id).DefaultIfEmpty().Max();
    public static void Initialise()
    {
        FreeCompanies      = new List<FreeCompany>(DatabaseManager.DataCentre.GetFreeCompanies());

        foreach (var freeCompany in FreeCompanies)
        {
            freeCompany.ChannelId = ChatChannelManager.CreateChatChannel(ChatChannelType.FreeCompanyChat);
        }
    }
    
    public static void Save()
    {
        foreach (var freeCompany in FreeCompanies)
        {
            var filter = Builders<FreeCompany>.Filter
                .Eq(r => r.Id, freeCompany.Id);

            DatabaseManager.DataCentre.FreeCompanies.ReplaceOne(filter, freeCompany, options: new ReplaceOptions { IsUpsert = true });
        }
    }
    
    public static FreeCompany CreateFreeCompany(Player leader, string name, string tag)
    {
        var freeCompany = new FreeCompany
        {
            Id = NextFreeCompanyId,
            Name = name,
            Tag = tag
        };
        FreeCompanies.Add(freeCompany);
        AddMember(freeCompany, leader);
        return freeCompany;
    }

    public static void AddMember(FreeCompany freeCompany, Player player)
    {
        freeCompany.Members.Add(player.Character.Id);
        player.SendFreeCompany();
        
    }

    public static FreeCompany GetFreeCompanyByID(ulong id)
    {
        return FreeCompanies.FirstOrDefault(f => f.Id == id);
    }
    
    public static FreeCompany GetFreeCompanyForPlayer(ulong id)
    {
        return FreeCompanies.FirstOrDefault(f => f.Members.Contains(id));
    }
}