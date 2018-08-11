using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync();

        Task<IEnumerable<TEntity>> GetListIncludingAsync(bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetOneAsync(int identifier);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false);

        Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> PutAsync(int identifier, TEntity entity);

        Task<bool> TryDeleteAsync(int identifier);

        Task<TEntity> PostAsync(TEntity entity);
    }
}
