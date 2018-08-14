using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class TeamService : CRUDService<Team, TeamDTO>, ITeamService
    {
        public TeamService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }

        public async Task<IEnumerable<TeamDTO>> GetManagerTeams(int managerId)
        {
            var manager = await uow.GetRepository<Manager>().GetAsync(managerId);
            if(manager != null)
            {
                var teams = (await uow.GetRepository<Project>()
                    .GetAllAsync(p => p.Manager.Id == managerId)
                    )
                    .SelectMany(p => p.Teams);

                if (teams != null)
                    return mapper.Map<IEnumerable<TeamDTO>>(teams);
            }
              return null;
        }
    }
}
