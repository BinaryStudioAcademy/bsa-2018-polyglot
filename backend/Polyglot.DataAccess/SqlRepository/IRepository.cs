using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IRepository <TEntity>: IBaseRepository<TEntity> where TEntity : Entity, new()
    {
     //   Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

       // Interfaces.IRepository<TEntity> Include(Expression<Func<TEntity, object>> include);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetLastAsync();

        //bool UpdateBool(TEntity entity); // Workaroud for Entity being tracked
        Task<bool> UpdateBool(TEntity entity); // Workaroud for Entity being tracked
    }
}
