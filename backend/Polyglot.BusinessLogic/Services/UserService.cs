using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.SqlRepository;

using Polyglot.DataAccess.Entities;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;

namespace Polyglot.BusinessLogic.Services
{
    public class UserService : CRUDService<UserProfile, UserProfileDTO>, IUserService
    {


        public UserService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }



        public async Task<UserProfileDTO> GetByUidAsync()
        {
            var user = await CurrentUser.GetCurrentUserProfile();
            if (user == null)
            {
                return null;
            }

            return mapper.Map<UserProfile, UserProfileDTO>(user);
        }

        public async Task<bool> IsExistByUidAsync(string uid)
        {
            var user = await CurrentUser.GetCurrentUserProfile();
            return user != null;
        }
    }
}
