using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.QueryTypes;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUnitOfWork _unitOfWork;

        public CurrentUser(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
        {
            _accessor = accessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfile> GetCurrentUserProfile()
        {

            if (RequestLevelCache["UserProfile"] == null)
            {
                var container = await CurrentUserProfileContainer();
                RequestLevelCache["UserProfile"] = container;
                return container;
            }
            var result = RequestLevelCache["UserProfile"] as UserProfile;
         return result;
        }

        public async Task<List<UserRights>> GetRightsInProject(int projId)
        {
            return (await GetRights()).Where(x => x.ProjectId == projId).ToList();
        }

        public async Task<List<UserRights>> GetRights()
        {
            if (RequestLevelCache["Rights"] == null)
            {
                var container = await GetRightsContainer();
                RequestLevelCache["Rights"] = container;
                return container;
            }
            var result = RequestLevelCache["Rights"] as List<UserRights>;
            return result;
        }

        private async Task<List<UserRights>> GetRightsContainer()
        {
            int userId = (await GetCurrentUserProfile()).Id;
            var userRights = (await _unitOfWork.GetViewData<UserRights>().GetAllAsync(r => r.UserId == userId));

            return userRights.ToList();
        }

        private async Task<UserProfile> CurrentUserProfileContainer()
        {
            UserProfile user = await _unitOfWork.GetRepository<UserProfile>().GetAsync(x => x.Uid == CurrentContext.User.GetUid());
            return user;
        }

        private IDictionary<object, object> RequestLevelCache
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

        public HttpContext CurrentContext => _accessor.HttpContext;
    }

   
}
