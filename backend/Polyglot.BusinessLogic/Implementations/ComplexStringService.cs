using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;


namespace Polyglot.BusinessLogic.Implementations
{
    public class ComplexStringService : IComplexStringService
    {
        private IRepository<ComplexString> _mongoRepository;
        private IRepository<Polyglot.DataAccess.Entities.ComplexString> _sqlRepository;
        private readonly IUnitOfWork _uow;

        public ComplexStringService(IRepository<Polyglot.DataAccess.Entities.ComplexString> sqlRepository, IRepository<ComplexString> mongoRepository, IUnitOfWork uow)
        {
            _sqlRepository = sqlRepository;
            _mongoRepository = mongoRepository;
            _uow = uow;
        }

        public Task<IEnumerable<ComplexString>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplexString>> GetListByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<ComplexString> GetOneAsync(int identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<ComplexString> PostAsync(ComplexString entity)
        {
            var sqlComplexString = new Polyglot.DataAccess.Entities.ComplexString
            {
                TranslationKey = entity.Key
            };
            var savedEntity= await _sqlRepository.CreateAsync(sqlComplexString);
            entity.Id = savedEntity.Id;
            await _mongoRepository.CreateAsync(entity);
            if ( _uow!= null)
            {
                await _uow.SaveAsync();
                return entity ?? null;
            }
            return null;
        }

        public Task<ComplexString> PutAsync(int identifier, ComplexString entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteAsync(int identifier)
        {
            throw new NotImplementedException();
        }
    }
}
