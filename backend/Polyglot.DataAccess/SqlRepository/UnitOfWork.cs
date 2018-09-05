using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.ViewsRepository;

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

        public IRepository<T> GetRepository<T>() where T : Entity, new() 
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

        public IMidRepository<T> GetMidRepository<T>() where T : MidEntity, new()
        {
            var targetType = typeof(T);
            if (repositories.ContainsKey(targetType))
            {
                return repositories[targetType] as IMidRepository<T>;
            }
            else
            {
                var repoInstance = new MidRepository<T>(context);
                repositories.Add(targetType, repoInstance);
                return repoInstance;
            }
        }

        public IViewData<T> GetViewData<T>()
            where T : QueryType, new()
        {
            var targetType = typeof(T);
            if (repositories.ContainsKey(targetType))
            {
                return repositories[targetType] as IViewData<T>;
            }
            else
            {
                var viewDataInstance = new ViewData<T>(context);
                repositories.Add(targetType, viewDataInstance);
                return viewDataInstance;
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
