using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Polyglot.BusinessLogic.Services
{
    public class GlossaryService : CRUDService<Glossary, GlossaryDTO>, IGlossaryService
    {
        public GlossaryService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public async Task<GlossaryDTO> AddString(int glossaryId, GlossaryStringDTO glossaryString)
        {
            var glossary = await uow.GetRepository<Glossary>().GetAsync(glossaryId);

            if (glossary == null)
                return null;
            glossary.GlossaryStrings.Add(mapper.Map<GlossaryString>(glossaryString));
            return mapper.Map<GlossaryDTO>(uow.GetRepository<Glossary>().Update(glossary));
        }

        public async Task<bool> DeleteString(int glossaryId, GlossaryStringDTO glossaryString)
        {
            var glossary = await uow.GetRepository<Glossary>().GetAsync(glossaryId);

            if (glossary == null)
                return false;
            glossary.GlossaryStrings.Remove(mapper.Map<GlossaryString>(glossaryString));
            return uow.GetRepository<Glossary>().Update(glossary) != null;
        }

        public async Task<GlossaryDTO> UpdateString(int glossaryId, GlossaryStringDTO glossaryString)
        {
            var glossary = await uow.GetRepository<Glossary>().GetAsync(glossaryId);

            if (glossary == null)
                return null;
            var target = glossary.GlossaryStrings.FirstOrDefault(s => s.Id == glossaryString.Id);
            glossary.GlossaryStrings.Remove(target);
            glossary.GlossaryStrings.Add(mapper.Map<GlossaryString>(glossaryString));
            return mapper.Map<GlossaryDTO>(uow.GetRepository<Glossary>().Update(glossary));
        }
    }
}
