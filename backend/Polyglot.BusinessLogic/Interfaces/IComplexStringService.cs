using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.BusinessLogic.Services;
using Polyglot.Common.DTOs.NoSQL;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IComplexStringService
    {
        Task<IEnumerable<ComplexStringDTO>> GetListAsync();

        Task<ComplexStringDTO> GetComplexString(int identifier);
        
        Task<ComplexStringDTO> ModifyComplexString(ComplexStringDTO entity);

        Task<bool> DeleteComplexString(int identifier);

        Task<ComplexStringDTO> AddComplexString(ComplexStringDTO entity);

        Task<IEnumerable<TranslationDTO>> GetStringTranslationsAsync(int identifier);

        Task<ComplexStringDTO> SetStringTranslations(int identifier, IEnumerable<TranslationDTO> translations);

        //Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<int> options);
    }
}
