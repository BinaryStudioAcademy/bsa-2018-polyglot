using MongoDB.Bson;
using MongoDB.Driver;
using Polyglot.DataAccess.NoSQL_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.NoSQL_Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        protected abstract IMongoCollection<TEntity> Collection { get; }

        public async Task Add(TEntity item)
        {
            try
            {
                await Collection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await Collection.FindAsync<TEntity>(new BsonDocument()).Result.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                var cursor = await Collection
                                .Find(x => x.Id == id)
                                .FirstOrDefaultAsync();
                return (TEntity)cursor;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult
                    = await Collection.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveById(int id)
        {
            try
            {
                DeleteResult actionResult
                    = await Collection.DeleteOneAsync(
                        Builders<TEntity>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            ReplaceOneResult updateResult = await Collection
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
            return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
        }
    }
}
