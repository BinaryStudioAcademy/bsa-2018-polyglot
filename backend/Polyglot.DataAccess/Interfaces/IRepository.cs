using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : Entity, new ()
    {
        //   Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> DeleteAsync(int id);

        Task<TEntity> PutAsync(int id);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        // Interfaces.IRepository<TEntity> Include(Expression<Func<TEntity, object>> include);
    }
}
