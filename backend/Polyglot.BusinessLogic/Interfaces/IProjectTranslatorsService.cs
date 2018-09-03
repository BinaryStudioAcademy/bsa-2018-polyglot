using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectTranslatorsService: ICRUDService<Project, ProjectDTO>
    {
        Task<IEnumerable<UserProfilePrevDTO>> GetProjectTranslators(int projectId);
    }
}
