using System;
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

        Task<TranslationDTO> SetStringTranslation(int identifier, TranslationDTO translation);

        Task<IEnumerable<CommentDTO>> SetComment(int identifier, CommentDTO comment, int itemsOnPage);
        
        Task<IEnumerable<CommentDTO>> DeleteComment(int identifier, Guid commentId);

        Task<IEnumerable<CommentDTO>> EditComment(int identifier, CommentDTO comment);

        Task<IEnumerable<CommentDTO>> GetCommentsAsync(int identifier);

        Task<IEnumerable<CommentDTO>> GetCommentsWithPaginationAsync(int id, int itemsOnPage, int page);

        Task<TranslationDTO> EditStringTranslation(int identifier, TranslationDTO translation);

        Task<IEnumerable<HistoryDTO>> GetHistoryAsync(int identifier, Guid translationId, int itemsOnPage, int page);

		Task<AdditionalTranslationDTO> AddOptionalTranslation(int stringId, Guid translationId, string value);

		Task<IEnumerable<OptionalTranslationDTO>> GetOptionalTranslations(int stringId, Guid translationId);

        Task ChangeStringStatus(int id, bool status, string groupName);
    }
}
