using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.DataAccess.MongoRepository
{
    public interface IMongoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
    {
       Task<TEntity> Update(TEntity entity);
    }
}
