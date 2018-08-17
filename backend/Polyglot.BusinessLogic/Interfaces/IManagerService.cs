using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IManagerService : ICRUDService<Manager, ManagerDTO>
    {
        //Task<IEnumerable<TeamDTO>> GetManagerTeams(int managerId);

        //Task<IEnumerable<ProjectDTO>> GetManagerProjects(int managerId);
    }
}
