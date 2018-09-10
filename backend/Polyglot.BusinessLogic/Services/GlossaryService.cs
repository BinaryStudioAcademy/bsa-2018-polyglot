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
            await uow.GetRepository<Glossary>().Update(glossary);
            await uow.SaveAsync();
            return mapper.Map<GlossaryDTO>(await uow.GetRepository<Glossary>().GetAsync(glossaryId));

        }

        public async Task<bool> DeleteString(int glossaryId, int glossaryStringId)
        {
            var glossary = await uow.GetRepository<Glossary>().GetAsync(glossaryId);

            if (glossary == null)
                return false;
            var target = glossary.GlossaryStrings.FirstOrDefault(s => s.Id == glossaryStringId);
            glossary.GlossaryStrings.Remove(target);
            await uow.GetRepository<Glossary>().Update(glossary);
            return await uow.SaveAsync() != 0;

        }

        public async Task<GlossaryDTO> UpdateString(int glossaryId, GlossaryStringDTO glossaryString)
        {
            var str = await uow.GetRepository<GlossaryString>().Update(mapper.Map<GlossaryString>(glossaryString));
            await uow.SaveAsync();
            return mapper.Map<GlossaryDTO>(await uow.GetRepository<Glossary>().GetAsync(glossaryId));
        }

        public override async Task<bool> TryDeleteAsync(int identifier)
        {
            if (uow != null)
            {
                var glossary = await uow.GetRepository<Glossary>().GetAsync(identifier);

                if (glossary == null)
                    return false;
                glossary.GlossaryStrings.Clear();
                await uow.GetRepository<Glossary>().Update(glossary);
                await uow.SaveAsync();
                await uow.GetRepository<Glossary>().DeleteAsync(identifier);
                await uow.SaveAsync();
                return true;
            }
            else
                return false;
        }

        public override async Task<GlossaryDTO> PostAsync(GlossaryDTO entity)
        {

            var ent = mapper.Map<Glossary>(entity);
            ent.OriginLanguage = null;
            var target = await uow.GetRepository<Glossary>().CreateAsync(ent);
            await uow.SaveAsync();

            return mapper.Map<GlossaryDTO>(target);
        }

        public async Task<IEnumerable<GlossaryDTO>> GetUsersGlossaries(int userId)
        {
            if (uow != null)
            {
                var targets = await uow.GetRepository<Glossary>().GetAllAsync();
                return mapper.Map<IEnumerable<GlossaryDTO>>(targets.Where(g => g.UserProfileId == userId));
            }
            else
                return null;
        }
    }
}
