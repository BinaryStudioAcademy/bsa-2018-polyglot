using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using System;
using System.Threading.Tasks;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using Action = Polyglot.Common.Helpers.SignalR.Action;
using Polyglot.BusinessLogic.Interfaces;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRNavigationService : SignalRCommonService<NavigationHub>, ISignaRNavigationService
    {
        private readonly IChatService chatService;

        public SignalRNavigationService(IHubContext<NavigationHub> hubContext, ICurrentUser  currentUser) : base(hubContext, currentUser)
        {
            this.chatService = chatService;
        }

        public async Task NotificationAdded(string groupName, int notificationId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.notificationSend.ToString(), await GetIdsResponce(notificationId));
        }

        public async Task NumberOfMessagesChanges(string groupName, int numberOfMessages)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.numberOfMessagesChanges.ToString(), numberOfMessages);
        }

    }
}
