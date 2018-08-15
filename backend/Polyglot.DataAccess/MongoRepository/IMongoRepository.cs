using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.DataAccess.MongoRepository
{
    public interface IMongoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
       Task<TEntity> Update(TEntity entity);
    }
}
