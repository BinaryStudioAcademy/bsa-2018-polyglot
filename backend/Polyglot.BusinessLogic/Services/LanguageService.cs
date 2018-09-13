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
    public class LanguageService :CRUDService<Language, LanguageDTO>, ILanguageService
    {
        public LanguageService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public async Task<IEnumerable<TranslatorLanguageDTO>> GetTranslatorLanguages(int userId)
        {
            var entity = await uow.GetMidRepository<TranslatorLanguage>().GetAllAsync(tr => tr.TranslatorId == userId);
            return mapper.Map<IEnumerable<TranslatorLanguageDTO>>(entity);
        }

        public async Task<IEnumerable<TranslatorLanguageDTO>> SetTranslatorLanguages(int userId, TranslatorLanguageDTO[] languages)
        {
            foreach (var language in languages)
            {
                var lang = await uow.GetMidRepository<TranslatorLanguage>().GetAsync(tl => tl.TranslatorId == userId && tl.Language.Id == language.Language.Id);
                if (lang != null)
                {
                    lang.Proficiency = language.Proficiency;
                    var entity = uow.GetMidRepository<TranslatorLanguage>().Update(mapper.Map<TranslatorLanguage>(lang));
                }
                else
                {
                    var entity = await uow.GetMidRepository<TranslatorLanguage>().CreateAsync(new TranslatorLanguage
                    {
                        LanguageId = language.Language.Id,
                        TranslatorId = userId,
                        Proficiency = language.Proficiency
                    });
                }
            }
            await uow.SaveAsync();
            return await this.GetTranslatorLanguages(userId);
        }

        public async Task<IEnumerable<TranslatorLanguageDTO>> DeleteTranslatorsLanguages(int userId, TranslatorLanguageDTO[] languages)
        {
            foreach (var language in languages)
            {
                var lang = await uow.GetMidRepository<TranslatorLanguage>().GetAsync(tl => tl.TranslatorId == userId && tl.Language.Id == language.Language.Id);
                if (lang != null)
                {
                    var entity = uow.GetMidRepository<TranslatorLanguage>().Delete(mapper.Map<TranslatorLanguage>(lang));
                }
            }
            await uow.SaveAsync();
            return await this.GetTranslatorLanguages(userId);
        }
    }
}
