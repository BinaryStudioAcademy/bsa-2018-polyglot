using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.BusinessLogic.Services;
using Polyglot.Common.DTOs;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IGlossaryService
    {
        Task<IEnumerable<GlossaryDTO>> GetListAsync();

        Task<GlossaryDTO> GetGlossary(int identifier);

        Task<GlossaryDTO> ModifyGlossary(GlossaryDTO entity);

        Task<bool> DeleteGlossary(int identifier);

        Task<GlossaryDTO> AddGlossary(GlossaryDTO entity);

        Task<GlossaryDTO> AssignProject(int glossaryId, int projectId);
    }
}