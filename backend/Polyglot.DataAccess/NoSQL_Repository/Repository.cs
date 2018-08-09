using MongoDB.Bson;
using MongoDB.Driver;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.DataAccess.NoSQL_Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected abstract IMongoCollection<TEntity> Collection { get; }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            try
            {
                await Collection.InsertOneAsync(item);
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
                return await Collection.FindAsync<TEntity>(new BsonDocument()).Result.ToListAsync();
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
                return (TEntity)cursor;
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

                return null;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                var updateResult =  Collection
                    .ReplaceOne<TEntity>(filter: g => g.Id == entity.Id, replacement: entity);
                return entity;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
