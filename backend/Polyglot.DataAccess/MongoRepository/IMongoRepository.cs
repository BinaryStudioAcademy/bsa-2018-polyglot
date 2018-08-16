using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
namespace Polyglot.DataAccess.MongoRepository
{
    public interface IMongoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
       Task<TEntity> Update(TEntity entity);

        long CountDocuments();

        void InsertMany(List<TEntity> entities);
    }
}
