using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class GlossaryService : IGlossaryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICRUDService<Glossary, GlossaryDTO> _userSevice;

        public GlossaryService(IUnitOfWork uow, IMapper mapper, ICRUDService<Glossary, GlossaryDTO> userSevice)
        {
            _uow = uow;
            _mapper = mapper;
            _userSevice = userSevice;
        }

        public async Task<GlossaryDTO> AddGlossary(GlossaryDTO entity)
        {
            if (entity != null)
            {
                var savedEntity = await _uow.GetRepository<Glossary>().CreateAsync(_mapper.Map<Glossary>(entity));
                await _uow.SaveAsync();
                return entity;
            }
            return null;
        }

        public async Task<GlossaryDTO> AssignProject(int glossaryId, int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteGlossary(int identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<GlossaryDTO> GetGlossary(int identifier)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GlossaryDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GlossaryDTO> ModifyGlossary(GlossaryDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}