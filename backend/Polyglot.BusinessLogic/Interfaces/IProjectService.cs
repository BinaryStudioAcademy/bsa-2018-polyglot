﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService : ICRUDService<Project,ProjectDTO>
    {
        Task FileParseDictionary(int id, IFormFile file);

        Task<IEnumerable<ProjectDTO>> GetListAsync(int userId);

        Task<ProjectDTO> PostAsync(ProjectDTO entity, int userId);
        
        Task<IEnumerable<LanguageDTO>> GetProjectLanguages(int id);

        Task<ProjectDTO> AddLanguagesToProject(int projectId, int[] languageIds);

        Task<bool> TryRemoveProjectLanguage(int projectId, int languageId);

        #region ComplexString

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id);

        Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync();

        #endregion
    }
}
