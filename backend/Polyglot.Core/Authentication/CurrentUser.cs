using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;

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

        private static async Task<UserProfile> CurrentUserProfileContainer()
        {
            IUnitOfWork unitOfWork = (IUnitOfWork)CurrentContext.RequestServices.GetService(typeof(IUnitOfWork));
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
