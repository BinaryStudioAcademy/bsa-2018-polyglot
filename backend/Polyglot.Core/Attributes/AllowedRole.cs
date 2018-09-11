using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Helpers;

namespace Polyglot.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowedRole: Attribute, IAuthorizationFilter
    {
        private readonly Role _allowedRole;

        public AllowedRole(Role allowedRole)
        {
            _allowedRole = allowedRole;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUserRole = (await CurrentUser.GetCurrentUserProfile()).UserRole;

            if (currentUserRole != _allowedRole)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
