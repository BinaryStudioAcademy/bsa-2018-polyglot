using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;
using System.IO;
using Polyglot;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService : ICRUDService<Project, ProjectDTO>
    {
        Task FileParseDictionary(int id, IFormFile file);

        Task<IEnumerable<ProjectDTO>> GetListAsync(int userId);

        Task<ProjectDTO> PostAsync(ProjectDTO entity, int userId);

        Task<IEnumerable<ActivityDTO>> GetAllActivitiesByProjectId(int id);

        Task<byte[]> GetFile(int id, int languageId, string format);

        #region Teams

        Task<IEnumerable<TeamPrevDTO>> GetProjectTeams(int projectId);

        Task<ProjectDTO> AssignTeamsToProject(int projectId, int[] teamIds);

        Task<bool> TryDismissProjectTeam(int projectId, int teamId);

        #endregion Teams

        #region Languages

        Task<IEnumerable<LanguageDTO>> GetProjectLanguages(int id);

        Task<ProjectDTO> AddLanguagesToProject(int projectId, int[] languageIds);

        Task<bool> TryRemoveProjectLanguage(int projectId, int languageId);

        #endregion Languages

        #region ComplexString

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id);

        Task<PaginatedStringsDTO> GetProjectStringsWithPaginationAsync(int id, int itemsOnPage, int page);

        Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync();

        Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<string> options, int projectId);

        #endregion

        Task<ProjectStatisticDTO> GetProjectStatistic(int id);
        Task<ChartDTO> GetTranskatedStringToLanguagesStatistic(int id);
        Task<ChartDTO> GetNotTranskatedStringToLanguagesStatistic(int id);
    }
}
