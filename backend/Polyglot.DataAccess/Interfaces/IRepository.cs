using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class
    {
     //   Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(int id);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

       // Interfaces.IRepository<TEntity> Include(Expression<Func<TEntity, object>> include);

        TEntity Update(TEntity entity);
    }
}
