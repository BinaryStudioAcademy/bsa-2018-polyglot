using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Polyglot.Authentication.Extensions;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Implementations;

using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
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
        
        public async Task SaveDate(ICRUDService<UserProfile, UserProfileDTO> service, HttpContext httpContext)
        {
            if (httpContext.User.GetUid() != null)
            {
                User.FullName = httpContext.User.GetName();
                User.Uid = httpContext.User.GetUid();
                User.AvatarUrl = httpContext.User.GetProfilePicture();

                IEnumerable<UserProfileDTO> users = await service.GetListAsync();
                UserProfileDTO userInDB = users.FirstOrDefault(x => x.Uid == User.Uid);

                if (userInDB == null)
                {
                    userInDB = await service.PostAsync(User);
                }
            }
        }

        public static UserProfileDTO GetCurrentUser()
        {
            return User;
        }
    }
}
