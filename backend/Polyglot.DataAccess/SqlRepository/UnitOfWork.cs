using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
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
        private Dictionary<Type, object> viewData;

        public UnitOfWork(DbContext c)
		{
			context = c;
            repositories = new Dictionary<Type, object>();
            viewData = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() 
            where T : DbEntity, new() 
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

        public IViewData<T> GetViewData<T>()
            where T : QueryType, new()
        {
            var targetType = typeof(T);
            if (viewData.ContainsKey(targetType))
            {
                return viewData[targetType] as IViewData<T>;
            }
            else
            {
                var viewDataInstance = new ViewData<T>(context);
                viewData.Add(targetType, viewDataInstance);
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
