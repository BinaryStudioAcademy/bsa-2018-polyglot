using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.Core.HttpServices
{
    public interface IHttpService<TEntity, TIdentifier> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<TEntity> GetOneAsync(TIdentifier identifier);
        Task PutAsync(TIdentifier identifier, TEntity entity);
        Task DeleteAsync(TIdentifier identifier);
        Task PostAsync(TEntity entity);
    }
}
