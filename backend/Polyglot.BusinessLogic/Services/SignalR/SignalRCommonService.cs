using Microsoft.AspNetCore.SignalR;
using Polyglot.Core.Authentication;
using Polyglot.Core.SignalR.Responses;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRCommonService<THub> where THub : Hub
    {
        protected readonly IHubContext<THub> hubContext;

        public SignalRCommonService(IHubContext<THub> hubContext)
        {
            this.hubContext = hubContext;
        }

        protected async Task<EntitiesIdsResponse> GetIdsResponce(params int[] ids)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            return new EntitiesIdsResponse()
            {
                SenderId = currentUser.Id,
                SenderFullName = currentUser.FullName,
                Ids = ids
            };
        }
    }
}
