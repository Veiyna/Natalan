using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Shared.Database
{
    public abstract class Database
    {
        public IMongoDatabase Data { get; private set; }

        private MongoClient Client;

        public virtual void Initialise(string host, uint port, string user, string password, string database)
        {
            
            var Settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, (int)port),
                LinqProvider = LinqProvider.V2
            };
            if (password != string.Empty)
                Settings.Credential = MongoCredential.CreateCredential(database, user, password);
            
            Client = new MongoClient(Settings);
            Data = this.Client.GetDatabase(database);
        }
        
    }
}
