using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.Common.DTOs.NoSQL;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService
    {
        

        
        Task FileParseDictionary(IFormFile file);

        #region Projects

        Task<ProjectDTO> GetProjectAsync(int id);

        Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync();

        Task<ProjectDTO> AddProjectAsync(ProjectDTO project);

        Task<ProjectDTO> ModifyProjectAsync(ProjectDTO project);

        Task<bool> TryDeleteProjectAsync(int id);
        #endregion

        #region ComplexString

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id);

        Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync();

        #endregion
    }
}
