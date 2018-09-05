using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IMidRepository<TEntity> where TEntity : MidEntity, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);

        TEntity Delete(TEntity entity);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync();

        TEntity Update(TEntity entity);
    }
}
