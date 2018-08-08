using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;

namespace Polyglot.DataAccess
{
    public class MongoDataContext : IMongoDataContext
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoDataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
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
