using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;

namespace Polyglot.DataAccess
{
    public class MongoDataContext
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoDataContext(Microsoft.Extensions.Options.IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                MongoDatabase = client.GetDatabase(settings.Value.Database);
        }
    }
}
