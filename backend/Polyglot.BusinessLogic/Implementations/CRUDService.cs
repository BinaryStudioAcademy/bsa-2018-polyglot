using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Implementations
{
    /// <summary>
    /// TEMP CLASS NOT FOR RELEASE !!!!!!!!!!!!111111111111111111
    /// </summary>
    /// <typeparam name="T"></typeparam>
#warning костыль
    public class CRUDService<T> : ICRUDService<T, int> where T : Entity, new()
    {
        private readonly IRepository<T> repository;
        private readonly IUnitOfWork uow;

        public CRUDService(IRepository<T> repository, IUnitOfWork uow)
        {
            this.repository = repository;
            this.uow = uow;
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await repository.GetAllAsync() ?? null;
        }

        
        public async Task<IEnumerable<T>> GetListIncludingAsync(bool isCached = false, params Expression<Func<T, object>>[] includeProperties)
        {
            return await repository.GetAllAsync() ?? null;
        }

        public async Task<T> GetOneAsync(int identifier)
        {
            return await repository.GetAsync(identifier) ?? null;
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool isCached = false)
        {
            return await repository.GetByAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindByIncludeAsync(Expression<Func<T, bool>> predicate, bool isCached = false, params Expression<Func<T, object>>[] includeProperties)
        {
            return await repository.GetByAsync(predicate);
        }

        public async Task<T> PostAsync(T entity)
        {
            await repository.CreateAsync(entity);
            if (uow != null)
            {
                await uow.SaveAsync();
                return entity ?? null;
            }
            return null;
        }

        public async Task<T> PutAsync(int identifier, T entity)
        {
            entity.Id = identifier;
            repository.Update(entity);
            if (uow != null)
            {
                await uow.SaveAsync();
                return entity ?? null;
            }
            return null;
        }

        public async Task<bool> TryDeleteAsync(int identifier)
        {
            await repository.DeleteAsync(identifier);
            if (uow != null)
            {
                await uow.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
