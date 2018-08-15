using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Polyglot.Authentication.Extensions;
using Polyglot.Common.DTOs;
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

        public void SaveDate(HttpContext httpContext)
        {
            if (httpContext.User.GetUid() != null)
            {
                User.FullName = httpContext.User.GetName();
                User.Uid = httpContext.User.GetUid();
                User.AvatarUrl = httpContext.User.GetProfilePicture();
            }
        }

        public static UserProfileDTO GetCurrentUser()
        {
            return User;
        }
    }
}
