using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class
    {
		Task<TEntity> GetAsync(int id);
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity> DeleteAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
