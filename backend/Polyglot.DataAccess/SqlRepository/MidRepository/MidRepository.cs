using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.SqlRepository
{
    public class MidRepository<TEntity> : IMidRepository<TEntity> where TEntity : MidEntity, new()
    {
        protected DbContext context;
        protected DbSet<TEntity> DbSet;

        private List<Expression<Func<TEntity, object>>> includeExpressions;

        public MidRepository(DbContext c)
        {
            this.context = c;
            DbSet = context.Set<TEntity>();
            includeExpressions = new List<Expression<Func<TEntity, object>>>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = (await DbSet.AddAsync(entity)).Entity;

            return result;
        }

        public TEntity Delete(TEntity entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
            //  return await ApplyIncludes().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
            //  return await ApplyIncludes().ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }
    }
}
