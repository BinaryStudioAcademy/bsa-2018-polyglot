using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ITeamService : ICRUDService<Team, TeamDTO>
    {
        Task<IEnumerable<TeamPrevDTO>> GetAllTeamsPrevs();

        Task<IEnumerable<TeammateDTO>> GetTeammates(int teamId);
    }
}
