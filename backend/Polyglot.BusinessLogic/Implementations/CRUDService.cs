using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
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
            return await repository.GetAll() ?? null;
        }

        public async Task<T> GetOneAsync(int identifier)
        {
            return await repository.Get(identifier) ?? null;
        }

        public async Task<T> PostAsync(T entity)
        {
            repository.Create(entity);
            await uow.SaveAsync();
            return entity ?? null;
        }

        public async Task<T> PutAsync(int identifier, T entity)
        {
            entity.Id = identifier;
            repository.Update(entity);
            await uow.SaveAsync();
            return entity ?? null;
        }

        public async Task<bool> TryDeleteAsync(int identifier)
        {
            repository.Delete(identifier);
            await uow.SaveAsync();
            return true;
        }
    }
}
