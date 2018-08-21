using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.DataAccess.SqlRepository
{
    public interface IRepository <TEntity>: IBaseRepository<TEntity> where TEntity : DbEntity
    {
     //   Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

       // Interfaces.IRepository<TEntity> Include(Expression<Func<TEntity, object>> include);

        TEntity Update(TEntity entity);
    }
}
