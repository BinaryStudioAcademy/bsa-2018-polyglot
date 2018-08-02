using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    interface IHttpService<TEntity, TIdentifier>
    {
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<List<TIdentifier>> GetByOneAsync(TIdentifier identifier);
        Task PutAsync(TIdentifier identifier, TEntity entity);
        Task DeleteAsync(TIdentifier identifier);
    }
}
