using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using System;
using System.Threading.Tasks;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using Action = Polyglot.Common.Helpers.SignalR.Action;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRNavigationService : SignalRCommonService<NavigationHub>, ISignaRNavigationService
    {
        public SignalRNavigationService(IHubContext<NavigationHub> hubContext, ICurrentUser  currentUser) : base(hubContext, currentUser)
        {
        }

        public async Task NotificationAdded(string groupName, int notificationId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.notificationSend.ToString(), await GetIdsResponce(notificationId));
        }
    }
}
