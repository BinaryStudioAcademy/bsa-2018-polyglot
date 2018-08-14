using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface ITeamService : ICRUDService<Team, TeamDTO>
    {
        Task<IEnumerable<TeamDTO>> GetManagerTeams(int managerId);

    }
}
