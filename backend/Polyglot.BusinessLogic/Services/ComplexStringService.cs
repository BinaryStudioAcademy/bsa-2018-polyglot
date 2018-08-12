using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;

namespace Polyglot.BusinessLogic.Services
{
    public class ComplexStringService : IComplexStringService
    {
        private readonly IMongoRepository<ComplexString> _repository;
        private readonly IMapper _mapper;

        public ComplexStringService(IMongoRepository<ComplexString> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetListAsync()
        {

            var targets = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ComplexStringDTO>>(targets);
        }

        public async Task<ComplexStringDTO> GetComplexString(int identifier)
        {

            var target = await _repository.GetAsync(identifier);
            if (target != null)
            {
                return _mapper.Map<ComplexStringDTO>(target);
            }

            return null;

        }

        public async Task<ComplexStringDTO> ModifyComplexString(ComplexStringDTO entity)
        {

            var target = await _repository.Update(_mapper.Map<ComplexString>(entity));
            if (target != null)
            {
                return _mapper.Map<ComplexStringDTO>(target);
            }
            return null;

        }

        public async Task<bool> DeleteComplexString(int identifier)
        {

            await _repository.DeleteAsync(identifier);

            return true;
        }

        public async Task<ComplexStringDTO> AddComplexString(ComplexStringDTO entity)
        {

            var target = await _repository
                .CreateAsync(_mapper.Map<ComplexString>(entity));
            if (target != null)
            {

                return _mapper.Map<ComplexStringDTO>(target);
            }
            return null;
        }
    }
}
