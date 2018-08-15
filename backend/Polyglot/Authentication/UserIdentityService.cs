using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Polyglot.Authentication.Extensions;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Authentication
{
    [Authorize]
    public class UserIdentityService
    {
        private static UserProfileDTO User { get; set; }

        public UserIdentityService()
        {
            User = new UserProfileDTO();
        }

        public async Task SaveDate(HttpContext httpContext)
        {
            if (httpContext.User.GetUid() != null)
            {
                User.FullName = httpContext.User.GetName();
                User.Uid = httpContext.User.GetUid();
                User.AvatarUrl = httpContext.User.GetProfilePicture();
                ICRUDService<UserProfile, UserProfileDTO> service = (ICRUDService<UserProfile, UserProfileDTO>)httpContext.RequestServices.GetService(typeof(ICRUDService<UserProfile, UserProfileDTO>));
                IEnumerable<UserProfileDTO> users = await service.GetListAsync();
                UserProfileDTO userInDB = users.FirstOrDefault(x => x.Uid == User.Uid);

                if (userInDB == null)
                {
                    userInDB = await service.PostAsync(User);
                    UserIdentityService.User = userInDB;
                }
            }
            else
            {
                User = new UserProfileDTO();
            }
        }

        public static UserProfileDTO GetCurrentUser()
        {
            return User;
        }
    }
}
