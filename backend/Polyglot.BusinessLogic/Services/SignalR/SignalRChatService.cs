using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRChatService : SignalRCommonService<ChatHub>, ISignalRChatService
    {
        public SignalRChatService(IHubContext<ChatHub> hubContext) : base(hubContext)
        {
        }

        public async Task MessageReveived(string groupName, ChatMessageDTO message)
        {
            await hubContext.Clients.Group(groupName).SendAsync(ChatAction.messageReceived.ToString(), message);
        }
    }
}
