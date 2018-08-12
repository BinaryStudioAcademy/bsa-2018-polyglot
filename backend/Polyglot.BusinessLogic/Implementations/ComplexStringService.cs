using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Xsl;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;
using ComplexString = Polyglot.DataAccess.NoSQL_Models.ComplexString;


namespace Polyglot.BusinessLogic.Implementations
{
    public class ComplexStringService : IComplexStringService
    {
        private readonly IComplexStringRepository _mongoRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ComplexStringService(IComplexStringRepository mongoRepository, IUnitOfWork uow, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ComplexStringDTO> PostAsync(ComplexStringDTO entity)
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

        public Task<ComplexStringDTO> PutAsync(ComplexStringDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
