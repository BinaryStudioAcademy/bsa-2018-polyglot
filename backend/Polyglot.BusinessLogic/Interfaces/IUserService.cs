using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IUserService : ICRUDService<UserProfile, UserProfileDTO>
    {
        Task<UserProfileDTO> GetByUidAsync();

        Task<bool> IsExistByUidAsync(string uid);

        Task<bool> PutUserBool(UserProfileDTO userProfileDTO);
    }
}
