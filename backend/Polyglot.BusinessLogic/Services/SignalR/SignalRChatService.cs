using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers.SignalR;
using System.Threading.Tasks;
using Polyglot.Core.Authentication;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRChatService : SignalRCommonService<ChatHub>, ISignalRChatService
    {
        public SignalRChatService(IHubContext<ChatHub> hubContext, ICurrentUser currentUser) : base(hubContext, currentUser)
        {
        }

        public async Task MessageReveived(string groupName, int dialogId, int messageId,  string text)
        {
            await hubContext.Clients.Group(groupName).SendAsync(ChatAction.messageReceived.ToString(), await GetChatMessageResponce(dialogId, messageId, text));
        }
    }
}
