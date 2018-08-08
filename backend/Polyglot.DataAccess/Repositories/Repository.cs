using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polyglot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

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
	}
}
