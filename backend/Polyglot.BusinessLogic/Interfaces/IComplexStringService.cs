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

<<<<<<< HEAD
        Task<ComplexStringDTO> SetStringTranslations(int identifier, IEnumerable<TranslationDTO> translations);

        //Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<int> options);
=======
        Task<TranslationDTO> SetStringTranslation(int identifier, TranslationDTO translation);

        Task<IEnumerable<CommentDTO>> SetComments(int identifier, IEnumerable<CommentDTO> comments);

        Task<IEnumerable<CommentDTO>> GetCommentsAsync(int identifier);


        Task<ComplexStringDTO> EditStringTranslation(int identifier, TranslationDTO translation);
>>>>>>> 65efe20b62a8974648f1746197154dbb665f2cd4
    }
}
