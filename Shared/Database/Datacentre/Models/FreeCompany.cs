using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using WorldServer.Game.FreeCompany.Enums;

namespace Shared.Database.Datacentre.Models
{
    [BsonIgnoreExtraElements]
    public class FreeCompany
    {
        public FreeCompany()
        {
        }
        public ulong Id { get; set; }
        
        public ulong ChannelId { get; set; }
        
        public ulong LeaderId { get; set; }
        public string Name { get; set; } = "";
        public string Tag { get; set; } = "";
        public ulong Credit { get; set; }
        public byte Rank { get; set; }
        public ulong Points { get; set; }
        public ulong Crest { get; set; }
        public uint CreateData { get; set; }
        public byte GC { get; set; }
        public ulong GCRep1 { get; set; }
        public ulong GCRep2 { get; set; }
        public ulong GCRep3 { get; set; }
        public FreeCompanyStatus Status { get; set; } = FreeCompanyStatus.Normal;
        public string Board { get; set; } = "";
        public string Slogan { get; set; } = "";
        public uint Version { get; set; }
        public ulong ActiveActions { get; set; }
        public ulong ActiveActionsTime { get; set; }
        public ulong Actions { get; set; }
        public List<ulong> Members { get; set; } = new();
        
    }
}