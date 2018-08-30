using Polyglot.BusinessLogic.Services;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IRightService : ICRUDService<Right, RightDTO>
    {
        Task<TranslatorDTO> SetTranslatorRight(int userId, int teamId, RightDefinition definition);

        Task<TranslatorDTO> RemoveTranslatorRight(int userId, int teamId, RightDefinition definition);

        Task<bool> CheckIfCurrentUserCanInProject(RightDefinition definition, int projectId);
    }
}
