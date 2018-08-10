using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.NoSQL_Models;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService // : ICRUDService<ProjectDTO,int>
    {
        

        
        Task FileParseDictionary(IFormFile file);

        #region Project

        Task<IEnumerable<Project>> GetAllProjectsAsync();

        #endregion

        #region ComplexString

        Task<IEnumerable<ComplexString>> GetProjectStringsAsync(int id);

        Task<IEnumerable<ComplexString>> GetAllStringsAsync();

        #endregion
    }
}
