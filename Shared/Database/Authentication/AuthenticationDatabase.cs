using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Shared.Database.Authentication
{
    public class AuthenticationDatabase : Database
    {
        
        public IMongoCollection<AccountInfo> AccountInfo;
        public IMongoCollection<ServiceAccountInfo> ServiceAccountInfo;

        public override void Initialise(string host, uint port, string user, string password, string database)
        {
            base.Initialise(host, port, user, password, database);
            this.AccountInfo = Data.GetCollection<AccountInfo>("Accounts");
            this.ServiceAccountInfo = Data.GetCollection<ServiceAccountInfo>("ServiceAccounts");

        }
        public bool CreateAccount(string username, string password, string salt)
        {
            // TODO: set session id to username till a proper launcher is created
            this.AccountInfo.InsertOne(new AccountInfo
            {
                Name = username,
                Password = password,
                salt = salt,
                session = username,

            });
            return true;
        }

        public async Task<uint?> GetAccount(string sessionId)
        {
            if (sessionId == string.Empty)
                return null;

            var accounts = this.AccountInfo.AsQueryable();
            var account = accounts.FirstOrDefault(a => a.session == sessionId);
            return account?.Id;

        }

        public bool CreateServiceAccount(uint accountId, string name)
        {
            this.ServiceAccountInfo.InsertOne(new ServiceAccountInfo
            {
                AccountId = accountId,
                Name = name,
            });
            
            return true;
        }

        public void SetAccountGameVersion(uint accountId, string version)
        {
            var filter = Builders<AccountInfo>.Filter
                .Eq(account => account.Id, accountId);

            var update = Builders<AccountInfo>.Update
                .Set(account => account.Version, version);

            this.AccountInfo.UpdateOne(filter, update);
        }
        
        public string GetAccountGameVersion(uint accountId)
        {
            var accounts = this.AccountInfo.AsQueryable();
            var account = accounts.FirstOrDefault(a => a.Id == accountId);
            return account.Version;
        }
        public async Task<List<ServiceAccountInfo>> GetServiceAccounts(uint accountId)
        {
            var serviceAccountList = this.ServiceAccountInfo.AsQueryable();
            
            
            return serviceAccountList.Where(a => a.AccountId == accountId).ToList();
        }

        public IEnumerable<VersionInfo> LoadVersionInfo()
        {
            return Data.GetCollection<VersionInfo>("Versions").AsQueryable().ToList();
        }
    }
}
