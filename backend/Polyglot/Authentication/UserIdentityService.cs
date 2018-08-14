using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Polyglot.Authentication.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Authentication
{
    [Authorize]
    public class UserIdentityService
    {
        public static string Name { get; set; }

        public static string Uid { get; set; }

        HttpContext httpContext;

        public void GetCurrentUser(HttpContext httpContext)
        {
            Name = httpContext.User.GetName();
            Uid = httpContext.User.GetUid();
        }
    }
}
