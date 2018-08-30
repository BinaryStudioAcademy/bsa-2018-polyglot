using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.MongoRepository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : Entity, new ()
    {
        string _collectionName;

        protected IMongoCollection<TEntity> Collection =>
            _dataContext.MongoDatabase.GetCollection<TEntity>(CollectionName);

        private readonly IMongoDataContext _dataContext;

        public string CollectionName
        {
            get => _collectionName ?? typeof(TEntity).Name;
            set => _collectionName = value;
        }

        public MongoRepository(IMongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            try
            {
                await Collection.InsertOneAsync(item);
                await ElasticsearchExtensions.UpdateSearchIndex(item, CrudAction.Create);
                return item;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                var result = await Collection.FindAsync<TEntity>(new BsonDocument());
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var result = await Collection.FindAsync<TEntity>(filter);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }



        public async Task<TEntity> GetAsync(int id)
        {
            try
            {
                var cursor = await Collection
                                .Find(x => x.Id == id)
                                .FirstOrDefaultAsync();
                return cursor;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        public async Task<TEntity> DeleteAsync(int id)
        {
            try
            {
                DeleteResult actionResult
                    = await Collection.DeleteOneAsync(
                        Builders<TEntity>.Filter.Eq("Id", id));
                await ElasticsearchExtensions.UpdateSearchIndex(new TEntity { Id = id }, CrudAction.Delete);
                return null;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                var updateResult = await Collection
                    .ReplaceOneAsync<TEntity>(filter: g => g.Id == entity.Id, replacement: entity);
                await ElasticsearchExtensions.UpdateSearchIndex(entity, CrudAction.Update);
                return entity;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public long CountDocuments()
        {
            return Collection.CountDocuments(FilterDefinition<TEntity>.Empty);
        }

        public void InsertMany(List<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }
    }
}
