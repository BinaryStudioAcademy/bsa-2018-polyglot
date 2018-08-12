using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService
    {
        Task<IEnumerable<TEntityDTO>> GetListAsync<TEntity, TEntityDTO>()
            where TEntity : Entity, new()
            where TEntityDTO : class, new();

        Task<TEntityDTO> GetOneAsync<TEntity, TEntityDTO>(int identifier)
            where TEntity : Entity, new()
            where TEntityDTO : class, new();
        
        Task<TEntityDTO> PutAsync<TEntity, TEntityDTO>(TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : class, new();

        Task<bool> TryDeleteAsync<TEntity>(int identifier)
            where TEntity : Entity, new();

        Task<TEntityDTO> PostAsync<TEntity, TEntityDTO>(TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : class, new();
    }
}
