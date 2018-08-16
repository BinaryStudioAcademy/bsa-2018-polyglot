using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class TeamsService : CRUDService<Team, TeamDTO>, ITeamService
    {
        public TeamsService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }

        public async Task<IEnumerable<TeamPrevDTO>> GetAllTeamsPrevs()
        {
            var teams = await uow.GetRepository<Team>().GetAllAsync();
            if (teams != null)
                return mapper.Map<IEnumerable<TeamPrevDTO>>(teams);

            return null;
        }

        public async Task<IEnumerable<TeammateDTO>> GetTeammates(int teamId)
        {
            var team = await uow.GetRepository<Team>().GetAsync(teamId);
            if (team != null)
                return mapper.Map<IEnumerable<TeammateDTO>>(team?.TeamTranslators);
            return null;
        }
    }
}
