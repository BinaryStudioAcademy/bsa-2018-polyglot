using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class
    {
		Task<TEntity> GetAsync(int id);

		Task<List<TEntity>> GetAllAsync();

		Task<TEntity> DeleteAsync(int id);

        Task<TEntity> CreateAsync(TEntity entity);

        TEntity Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllIncludingAsync(bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false);

        Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
