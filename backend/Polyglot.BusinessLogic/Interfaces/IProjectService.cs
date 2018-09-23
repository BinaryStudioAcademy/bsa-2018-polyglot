using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService : ICRUDService<Project, ProjectDTO>
    {
        Task FileParseDictionary(int id, IFormFile file);

        Task<IEnumerable<ActivityDTO>> GetAllActivitiesByProjectId(int id);

        Task<byte[]> GetFile(int id, int languageId, string format);
		Task<byte[]> GetFullLocal(int id, string format);

        Task IncreasePriority(int projectId);

        #region Teams

        Task<IEnumerable<TeamPrevDTO>> GetProjectTeams(int projectId);

        Task<ProjectDTO> AssignTeamsToProject(int projectId, int[] teamIds);

        Task<bool> TryDismissProjectTeam(int projectId, int teamId);

        #endregion Teams

        #region Languages

        Task<IEnumerable<LanguageDTO>> GetProjectLanguages(int projectId);

        Task<IEnumerable<LanguageStatisticDTO>> GetProjectLanguagesStatistic(int projectId);
        
     //   Task<IEnumerable<LanguageStatisticDTO>> GetProjectLanguagesStatistic(int projectId, int[] languageIds);

        Task<LanguageStatisticDTO> GetProjectLanguageStatistic(int projectId, int languageId);

        Task<ProjectDTO> AddLanguagesToProject(int projectId, int[] languageIds);

        Task<bool> TryRemoveProjectLanguage(int projectId, int languageId);

        #endregion Languages

        #region ComplexString

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id);

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsWithPaginationAsync(int id, int itemsOnPage, int page, string search);

        Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync();

        Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<string> options, int projectId);

        #endregion



        #region Glossary

        Task<ProjectDTO> AssignGlossaries(int projectId, int[] glossaryIds);

        Task<IEnumerable<GlossaryDTO>> GetAssignedGlossaries(int projectId);

        Task<IEnumerable<GlossaryDTO>> GetNotAssignedGlossaries(int projectId);

        Task<bool> TryDismissGlossary(int projectId, int glossaryId);

        #endregion

        Task<ProjectStatisticDTO> GetProjectStatistic(int id);
        Task<ChartDTO> GetTranskatedStringToLanguagesStatistic(int id);
        Task<ChartDTO> GetNotTranskatedStringToLanguagesStatistic(int id);
        Task<ProjectTranslationStatisticsDTO> GetProjectLanguageStatistic(int projectId);
        Task<IEnumerable<ProjectTranslationStatisticsDTO>> GetProjectLanguageStatistics(List<int> projectIds);

		Task<List<ProjectDTO>> SearchProjects(string query);
    }
}