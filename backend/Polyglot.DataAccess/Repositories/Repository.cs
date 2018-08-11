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
	public class Repository<TEntity> :  IRepository<TEntity> where TEntity : class
	{
		protected DbContext context;
		protected DbSet<TEntity> DbSet;

        private List<Expression<Func<TEntity, object>>> includeExpressions;

		public Repository(DbContext c)
		{
			this.context = c;
			DbSet = context.Set<TEntity>();
            includeExpressions = new List<Expression<Func<TEntity, object>>>();
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

        public async Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await ApplyIncludes().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await ApplyIncludes().ToListAsync();
        }

        public TEntity Update(TEntity entity)
		{
			return DbSet.Update(entity).Entity;			
		}

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) 
            => 
            ApplyIncludes().AnyAsync(where);

        public IRepository<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            includeExpressions.Add(include);
            return this;
        }

        protected IQueryable<TEntity> ApplyIncludes()
            => 
            includeExpressions
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(DbSet, (current, expression) => current.Include(expression));

    }
}
