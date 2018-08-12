using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService<TEntity, TIdentifier> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync();

        Task<IEnumerable<TEntity>> GetListIncludingAsync(bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetOneAsync(TIdentifier identifier);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false);

        Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> PutAsync(TIdentifier identifier, TEntity entity);

        Task<bool> TryDeleteAsync(TIdentifier identifier);

        Task<TEntity> PostAsync(TEntity entity);
    }
}
