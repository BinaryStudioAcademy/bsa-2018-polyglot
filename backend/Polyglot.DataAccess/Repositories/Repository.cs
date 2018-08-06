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

		public void Create(TEntity entity)
		{
			DbSet.AddAsync(entity);			
		}

		public void Delete(int id)
		{
			TEntity temp = DbSet.Find(id);
			if(temp != null)
			{
				DbSet.Remove(temp);				
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

		public void Update(TEntity entity)
		{
			DbSet.Update(entity);			
		}
	}
}
