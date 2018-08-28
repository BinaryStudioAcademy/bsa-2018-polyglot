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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity, new()
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
            var result = (await DbSet.AddAsync(entity)).Entity;
            await ElasticsearchExtensions.UpdateSearchIndex(result, CrudAction.Create);
            //if (result is ISearcheable)
            //{
            //    var elastic = ElasticsearchExtensions.GetElasticClient();
            //    var ent = result as Project;
            //    var test = new Project
            //    {
            //        Id = ent.Id,
            //        CreatedOn = ent.CreatedOn,
            //        Description = ent.Description,
            //        Name = ent.Name
            //    };
            //    await elastic.IndexDocumentAsync(test);
            //}

            return result;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            TEntity temp = await DbSet.FindAsync(id);
            if (temp != null)
            {
                return DbSet.Remove(temp).Entity;
            }
            return null;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
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

        public bool UpdateBool(TEntity entity)
        {
            var entityDb = entity as Entity;
            if (entityDb != null)
            {
                var existingEntity = DbSet.Find(entityDb.Id);
                if (existingEntity == null)
                    return false;

                context.Entry(existingEntity).State = EntityState.Detached;
            }

            context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        //public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) 
        //    => 
        //    ApplyIncludes().AnyAsync(where);

        //public IRepository<TEntity> Include(Expression<Func<TEntity, object>> include)
        //{
        //    includeExpressions.Add(include);
        //    return this;
        //}

        //protected IQueryable<TEntity> ApplyIncludes()
        //    => 
        //    includeExpressions
        //        .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(DbSet, (current, expression) => current.Include(expression));

    }
}
