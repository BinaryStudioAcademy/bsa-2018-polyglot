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
        protected readonly MongoDataContext _context = null;

        public Repository(Microsoft.Extensions.Options.IOptions<Settings> settings)
        {
            _context = new MongoDataContext(settings);
        }

        public async Task Add(TEntity item)
        {
            try
            {
                await _context.Entities.InsertOneAsync(item);
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
                return await _context.Entities.FindAsync<TEntity>(new BsonDocument()).Result.ToListAsync();
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
                var cursor = await _context.Entities
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
                    = await _context.Entities.DeleteManyAsync(new BsonDocument());

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
                    = await _context.Entities.DeleteOneAsync(
                        Builders<IEntity>.Filter.Eq("Id", id));

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
            ReplaceOneResult updateResult = await _context.Entities
                .ReplaceOneAsync(filter: g => g.Id == entity.Id, replacement: entity);
            return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
        }
    }
}
