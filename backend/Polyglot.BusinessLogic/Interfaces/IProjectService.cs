using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService : ICRUDService<Project,ProjectDTO>
    {
        

        
        Task FileParseDictionary(IFormFile file);

       
        #region ComplexString

        Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id);

        Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync();

        #endregion
    }
}
