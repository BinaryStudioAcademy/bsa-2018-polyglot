using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ILanguageService : ICRUDService<Language, LanguageDTO>
    {
        Task<IEnumerable<TranslatorLanguageDTO>> GetTranslatorLanguages(int userId);

        Task<IEnumerable<TranslatorLanguageDTO>> SetTranslatorLanguages(int userId, TranslatorLanguageDTO[] languages);
    }
}
