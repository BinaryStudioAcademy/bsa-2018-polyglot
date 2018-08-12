using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Polyglot.DataAccess.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected DataContext context;
		protected DbSet<TEntity> DbSet;

		public Repository(DataContext c)
		{
			this.context = c;
			DbSet = context.Set<TEntity>();
		}

		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			return (await DbSet.AddAsync(entity)).Entity;			
		}

		public async Task<TEntity> DeleteAsync(int id)
		{
			TEntity temp = await DbSet.FindAsync(id);
			if(temp != null)
			{
				return DbSet.Remove(temp).Entity;				
			}
            return null;
		}

		public async Task<TEntity> GetAsync(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public async Task<List<TEntity>> GetAllAsync()
		{
			return await DbSet.ToListAsync();
		}

		public TEntity Update(TEntity entity)
		{
			return DbSet.Update(entity).Entity;			
		}

        public async Task<IEnumerable<TEntity>> GetAllIncludingAsync(bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AllInclude(isCached, includeProperties)
                .ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false)
        {
            IQueryable<TEntity> queryable; 

            if (isCached)
            {
                queryable = DbSet.AsTracking();
            }
            else
            {
                queryable = DbSet.AsNoTracking();
            }

            return await queryable.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await AllInclude(isCached, includeProperties)
                .Where(predicate)
                .ToListAsync();
        }

        protected IQueryable<TEntity> AllInclude(bool isCached = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = isCached ? DbSet.AsTracking() : DbSet.AsNoTracking();
            return includeProperties
                .Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
