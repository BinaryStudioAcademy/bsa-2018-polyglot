using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                    var entity = uow.GetMidRepository<TranslatorLanguage>().Update(mapper.Map<TranslatorLanguage>(language));
                }
                else
                {
                    var entity = await uow.GetMidRepository<TranslatorLanguage>().CreateAsync(mapper.Map<TranslatorLanguage>(language));
                }
            }
            var all = await uow.GetMidRepository<TranslatorLanguage>().GetAllAsync(tr => tr.TranslatorId == userId);
            return mapper.Map<IEnumerable<TranslatorLanguageDTO>>(all);
        }
    }
}
