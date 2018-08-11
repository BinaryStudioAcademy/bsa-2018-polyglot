using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.NoSQL_Models;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService
    {
        Task<IEnumerable<TEntityDTO>> GetListAsync<TEntity, TEntityDTO>()
            where TEntity : Entity, new()
            where TEntityDTO : IEntity, new();

        Task<TEntityDTO> GetOneAsync<TEntity, TEntityDTO>(int identifier)
            where TEntity : Entity, new()
            where TEntityDTO : IEntity, new();
        
        Task<TEntityDTO> PutAsync<TEntity, TEntityDTO>(int identifier, TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : IEntity, new();

        Task<bool> TryDeleteAsync<TEntity>(int identifier)
            where TEntity : Entity, new();

        Task<TEntityDTO> PostAsync<TEntity, TEntityDTO>(TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : IEntity, new();
    }
}
