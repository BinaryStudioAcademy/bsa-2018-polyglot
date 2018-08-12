using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic.Implementations
{
    public class CRUDService : ICRUDService
    {
        protected readonly IUnitOfWork uow;
        protected readonly IMapper mapper;

        public CRUDService(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<IEnumerable<TEntityDTO>> GetListAsync<TEntity, TEntityDTO>()
            where TEntity : Entity, new()
            where TEntityDTO : class, new()
        {
            if (uow != null)
            {
                var targets = await uow.GetRepository<TEntity>().GetAllAsync();
                return mapper.Map<IEnumerable<TEntityDTO>>(targets);
            }
            else
                return null;
        }

        public async Task<TEntityDTO> GetOneAsync<TEntity, TEntityDTO>(int identifier)
            where TEntity : Entity, new()
            where TEntityDTO : class, new()
        {
            if (uow != null)
            {
                var target =  await uow.GetRepository<TEntity>().GetAsync(identifier);
                if (target != null)
                    return mapper.Map<TEntityDTO>(target);
            }
            return null;
        }

        public async Task<TEntityDTO> PutAsync<TEntity, TEntityDTO>(TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : class, new()
        {
            if (uow != null)
            {
                var target = uow.GetRepository<TEntity>().Update(mapper.Map<TEntity>(entity));
                if(target != null)
                {
                    await uow.SaveAsync();
                    return mapper.Map<TEntityDTO>(target);
                }
            }
            return null;
        }

        public async Task<bool> TryDeleteAsync<TEntity>(int identifier) where TEntity : Entity, new()
        {
            if (uow != null)
            {
                await uow.GetRepository<TEntity>().DeleteAsync(identifier);
                await uow.SaveAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<TEntityDTO> PostAsync<TEntity, TEntityDTO>(TEntityDTO entity)
            where TEntity : Entity, new()
            where TEntityDTO : class, new()
        {
            if (uow != null)
            {
                var target = await uow.GetRepository<TEntity>()
                    .CreateAsync(mapper.Map<TEntity>(entity));
                if(target != null)
                {
                    await uow.SaveAsync();
                    return mapper.Map<TEntityDTO>(target);
                }
            }
            return null;
        }
    }
}
