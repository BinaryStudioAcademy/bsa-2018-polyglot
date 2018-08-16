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
    public class ManagerService : CRUDService<Manager, ManagerDTO>, IManagerService
    {
        public ManagerService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }

        //public async Task<IEnumerable<TeamDTO>> GetManagerTeams(int managerId)
        //{
        //    var manager = await uow.GetRepository<Manager>()
        //        .GetAsync(managerId);

        //    if(manager != null)
        //    {
        //        var teams = manager.Projects
        //            ?.SelectMany(p => p.Teams);

        //        if (teams != null)
        //            return mapper.Map<IEnumerable<TeamDTO>>(teams);
        //    }
        //      return null;
        //}

        //public async Task<IEnumerable<ProjectDTO>> GetManagerProjects(int managerId)
        //{
        //    var projects = await uow.GetRepository<Project>()
        //        .GetAllAsync(p => p.Manager.Id == managerId);

        //    if (projects != null)
        //        return mapper.Map<IEnumerable<ProjectDTO>>(projects);
        //    return null;
        //}
    }
}
