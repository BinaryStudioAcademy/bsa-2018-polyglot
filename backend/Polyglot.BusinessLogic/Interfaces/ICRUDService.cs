using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService<TEntity, TIdentifier> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync();

        Task<TEntity> GetOneAsync(TIdentifier identifier);

        Task<TEntity> PutAsync(TIdentifier identifier, TEntity entity);

        Task<bool> TryDeleteAsync(TIdentifier identifier);

        Task<TEntity> PostAsync(TEntity entity);
    }
}
