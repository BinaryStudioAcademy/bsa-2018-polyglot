using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class ProjectTranslatorsService: CRUDService<Project, ProjectDTO>, IProjectTranslatorsService
    {
        public ProjectTranslatorsService(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper)
        {
        }

        public async Task<IEnumerable<UserProfilePrevDTO>> GetProjectTranslators(int projectId)
        {
            var project = await uow.GetRepository<Project>()
                    .GetAsync(projectId);

            if (project == null)
                return null;

            var projectTeams = project.ProjectTeams;
            var users = new List<UserProfile>();

            projectTeams.ToList().ForEach(projectTeam =>
                projectTeam.Team.TeamTranslators.ToList().ForEach(translator =>
                users.Add(translator.UserProfile)));

            users = ((IEnumerable<UserProfile>)users).Distinct().ToList();

            return mapper.Map<IEnumerable<UserProfilePrevDTO>>(users);
        }
    }
}
