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
		void DeleteAsync(int id);
		void CreateAsync(TEntity entity);
		void Update(TEntity entity);
    }
}
