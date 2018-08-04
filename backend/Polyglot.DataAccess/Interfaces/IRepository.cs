using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class
    {
		Task<TEntity> Get(int id);
		Task<List<TEntity>> GetAll();
		Task Delete(int id);
		Task Create(TEntity entity);
		Task Update(TEntity entity);
    }
}
