using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ICRUDService <TEntity, TEntityDTO>
		where TEntity : Entity, new()
		where TEntityDTO : class, new()
	{
		Task<IEnumerable<TEntityDTO>> GetListAsync();
 

		Task<TEntityDTO> GetOneAsync(int identifier);
     
		Task<TEntityDTO> PutAsync(TEntityDTO entity);
         

		Task<bool> TryDeleteAsync(int identifier);

        Task<bool> TryAddAsync(int identifier);


        Task<TEntityDTO> PostAsync(TEntityDTO entity);
            
    }
}
