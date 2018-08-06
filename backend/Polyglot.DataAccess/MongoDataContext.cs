using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;

namespace Polyglot.DataAccess
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDataContext(Microsoft.Extensions.Options.IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<IEntity> Entities => _database.GetCollection<IEntity>("ComplexStrings");
    }
}
