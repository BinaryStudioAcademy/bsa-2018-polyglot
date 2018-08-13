using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.DataAccess.MongoRepository
{
    public class MongoDataContext : IMongoDataContext
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoDataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            MongoDatabase = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<ComplexString> ComplexStrings
        {
            get
            {
                return MongoDatabase.GetCollection<ComplexString>("ComplexStrings");
            }
        }
    }
}
