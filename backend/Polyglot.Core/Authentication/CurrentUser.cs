using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.Helpers;
using System.Linq;

namespace Polyglot.Core.Authentication
{
    public static class CurrentUser
    {
        public static async Task<UserProfile> GetCurrentUserProfile()
        {

            if (RequestLevelCache["CurrentServiceProfile"] == null)
            {
                var container = await CurrentUserProfileContainer();
                RequestLevelCache["CurrentServiceProfile"] = container;
                return container;
            }
            var result = RequestLevelCache["CurrentServiceProfile"] as UserProfile;
         return result;
        }

        public static async Task<List<RightDefinition>> GetRightsInProject(int projId)
        {
            IUnitOfWork unitOfWork = (IUnitOfWork)CurrentContext?.RequestServices?.GetService(typeof(IUnitOfWork));
            int userId = (await GetCurrentUserProfile()).Id;
            var userRights = (await unitOfWork.GetViewData<UserRights>().GetAllAsync())
                .Where(r => r.ProjectId == projId && r.UserId == userId)
                .Select(r => r.RightDefinition);

            return userRights.ToList();
        }

        public static async Task<List<RightDefinition>> GetRights()
        {
            IUnitOfWork unitOfWork = (IUnitOfWork)CurrentContext?.RequestServices?.GetService(typeof(IUnitOfWork));
            int userId = (await GetCurrentUserProfile()).Id;
            var userRights = (await unitOfWork.GetViewData<UserRights>().GetAllAsync())
                .Where(r => r.UserId == userId)
                .Select(r => r.RightDefinition)
                .Distinct();

            return userRights.ToList();
        }

        private static async Task<UserProfile> CurrentUserProfileContainer()
        {
            IUnitOfWork unitOfWork = (IUnitOfWork)CurrentContext?.RequestServices?.GetService(typeof(IUnitOfWork));
            UserProfile user = await unitOfWork.GetRepository<UserProfile>().GetAsync(x => x.Uid == CurrentContext.User.GetUid());
            return user;
        }

        private static IDictionary<object, object> RequestLevelCache
        {
            get
            {
                var ctx = CurrentContext;
                if (ctx == null)
                {
                    throw new NotSupportedException("No request level cache: If unit testing, use InjectRequestLevelAndSessionCacheForTesting().");
                }
                return ctx.Items;
            }
        }

        public static HttpContext CurrentContext { private get; set; }
    }
}
