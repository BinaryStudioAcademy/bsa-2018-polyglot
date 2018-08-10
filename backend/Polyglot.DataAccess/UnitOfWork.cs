using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
		private DataContext context;
        private Dictionary<Type, object> repositories;

		public UnitOfWork(DataContext c)
		{
			context = c;
		}

        public IRepository<T> GetRepository<T>() where T : Entity, new()
        {
            var targetType = typeof(T);
            if (repositories.ContainsKey(targetType))
            {
                return repositories[targetType] as IRepository<T>;
            }
            else
            {
                var repoInstance =  (IRepository<T>)Activator.CreateInstance(typeof(IRepository<T>), context);
                repositories.Add(targetType, repoInstance);
                return repoInstance;
            }
        }

		public async Task<int> SaveAsync()
		{
			return await context.SaveChangesAsync();
		}

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
