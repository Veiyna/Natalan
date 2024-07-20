using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shared.Database.Datacentre.Models;

namespace Shared.Database.Datacentre
{
    public class DataCentreDatabase : Database
    {

        public IMongoCollection<CharacterInfo> Characters;
        public IMongoCollection<RealmInfo> RealmInfo;
        public IMongoCollection<FreeCompany> FreeCompanies;

        public override void Initialise(string host, uint port, string user, string password, string database)
        {
            base.Initialise(host, port, user, password, database);
            this.Characters = Data.GetCollection<CharacterInfo>("Characters");
            this.FreeCompanies = Data.GetCollection<FreeCompany>("FreeCompanies");
            this.RealmInfo =  Data.GetCollection<RealmInfo>("RealmInfo");
            
        }

        public IEnumerable<RealmInfo> GetRealmList()
        {
            return this.RealmInfo.AsQueryable().ToList();
        }
        
        public async Task Save(Action<DataCentreDatabase> action)
        {
            action.Invoke(this);
        }

        public ulong GetMaxCharacterId()
        {
            var items = this.Characters.AsQueryable();
            return items.ToList()
                .Select(r => r.Id).DefaultIfEmpty()
                .Max();
        }
        
        public async Task<List<CharacterInfo>> GetCharacters(uint serviceAccountId)
        {
            var characters = this.Characters.AsQueryable().Where(c => c.AccountId == serviceAccountId).ToList();
            return characters;
        }
        
        public IEnumerable<FreeCompany> GetFreeCompanies()
        {
            return this.FreeCompanies.AsQueryable().ToList();
        }

        
        public CharacterInfo GetCharacterById(ulong id)
        {
            var characters = this.Characters.AsQueryable();
            return characters.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> IsCharacterNameAvailable(string name)
        {
            var characters = this.Characters.AsQueryable();
            return characters.All(c => c.Name != name);
        }

        public async Task CreateCharacterSession(ulong id, string source)
        {
        }
        
        public ulong GetNextItemId()
        {
            var items = Data.GetCollection<ItemModel>("Items").AsQueryable();
            return items.ToList()
                .Select(r => r.Id).DefaultIfEmpty()
                .Max();
        }

        public async Task<(uint ServiceAccountId, ulong CharacterId)> GetCharacterSession(uint actorId, string source)
        {
            var characters = this.Characters.AsQueryable();
            var character = characters.First(c => c.ActorId == actorId);
            return (character.AccountId, character.Id);
        }
    }
}
