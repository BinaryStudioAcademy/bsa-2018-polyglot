using MongoDB.Driver;

namespace Polyglot.DataAccess.MongoRepository
{
    public interface IMongoDataContext
    {
        IMongoDatabase MongoDatabase { get; }
    }
}
