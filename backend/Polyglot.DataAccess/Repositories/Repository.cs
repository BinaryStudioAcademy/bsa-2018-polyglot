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


		public async Task Create(TEntity entity)
		{
			await DbSet.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			TEntity temp = await DbSet.FindAsync(id);
			if(temp != null)
			{
				DbSet.Remove(temp);
				await context.SaveChangesAsync();
			}			
		}

		public async Task<TEntity> Get(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public async Task<List<TEntity>> GetAll()
		{
			return await DbSet.ToListAsync();
		}

		public async Task Update(TEntity entity)
		{
			DbSet.Update(entity);
			await context.SaveChangesAsync();
		}
	}
}
