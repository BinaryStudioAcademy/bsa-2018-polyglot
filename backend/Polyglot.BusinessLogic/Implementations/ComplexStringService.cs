using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;


namespace Polyglot.BusinessLogic.Implementations
{
    public class ComplexStringService : IComplexStringService
    {
        private readonly IMongoRepository<ComplexString> _mongoRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ComplexStringService(IMongoRepository<ComplexString> mongoRepository, IUnitOfWork uow, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ComplexStringDTO> AddComplexString(ComplexStringDTO entity)
        {
            var sqlComplexString = new Polyglot.DataAccess.Entities.ComplexString
            {
                TranslationKey = entity.Key
            };
            var savedEntity = await _uow.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().CreateAsync(sqlComplexString);
            await _uow.SaveAsync();
            entity.Id = savedEntity.Id;
            await _mongoRepository.CreateAsync(_mapper.Map<ComplexStringDTO, ComplexString>(entity));
            return null;
        }

        public Task<IEnumerable<ComplexStringDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComplexStringDTO> GetComplexString(int identifier)
        {
            throw new NotImplementedException();
        }

        public Task<ComplexStringDTO> ModifyComplexString(ComplexStringDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComplexString(int id)
        {
            throw new NotImplementedException();
        }
    }
}
