using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Polyglot.DataAccess.SqlRepository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
		private DbContext context;
        private Dictionary<Type, object> repositories;

		public UnitOfWork(DbContext c)
		{
			context = c;
            repositories = new Dictionary<Type, object>();

        }

        public IRepository<T> GetRepository<T>() 
            where T : Entity, new()
        {
            var targetType = typeof(T);
            if (repositories.ContainsKey(targetType))
            {
                return repositories[targetType] as IRepository<T>;
            }
            else
            {
                var repoInstance = new Repository<T>(context);
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
