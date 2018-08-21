using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IUserService : ICRUDService<UserProfile, UserProfileDTO>
    {
        Task<UserProfileDTO> GetByUidAsync(string uid);

        Task<bool> IsExistByUidAsync(string uid);
    }
}
