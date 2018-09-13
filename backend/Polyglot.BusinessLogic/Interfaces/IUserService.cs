using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IUserService : ICRUDService<UserProfile, UserProfileDTO>
    {
        Task<UserProfileDTO> GetByUidAsync();

        Task<bool> IsExistByUidAsync();

        Task<bool> PutUserBool(UserProfileDTO userProfileDTO);

        Task<IEnumerable<UserProfileDTO>> GetUsersByNameStartsWith(string startsWith);
    }
}
