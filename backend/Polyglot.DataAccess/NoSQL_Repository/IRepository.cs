using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.NoSQL_Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<bool> RemoveById(int id);

        Task<bool> RemoveAll();
    }
}
