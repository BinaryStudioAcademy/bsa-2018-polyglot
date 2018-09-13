using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.QueryTypes;

namespace Polyglot.Core.Authentication
{
    public interface ICurrentUser
    {
        Task<UserProfile> GetCurrentUserProfile();

        Task<List<UserRights>> GetRightsInProject(int projId);

        Task<List<UserRights>> GetRights();
    }
}
