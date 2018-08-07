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
		void Delete(int id);
		void Create(TEntity entity);
		void Update(TEntity entity);
    }
}
