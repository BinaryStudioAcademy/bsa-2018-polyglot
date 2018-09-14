using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers.SignalR;
using System.Threading.Tasks;
using Polyglot.Core.Authentication;
using Polyglot.Core.SignalR.Responses;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRChatService : SignalRCommonService<ChatHub>, ISignalRChatService
    {
        public SignalRChatService(IHubContext<ChatHub> hubContext, ICurrentUser currentUser) : base(hubContext, currentUser)
        {
        }

        public async Task MessageReveived(string groupName, int dialogId, int messageId, string text)
        {
#warning GetChatMessageResponce не работает из-за currentUser == null
            await hubContext.Clients.Group(groupName).SendAsync(ChatAction.messageReceived.ToString(), await GetChatMessageResponce(dialogId, messageId, text));
        }

        public async Task MessageReveived(string groupName, ChatMessageResponce data)
        {
            await hubContext.Clients.Group(groupName).SendAsync(ChatAction.messageReceived.ToString(), data);
        }

        public async Task DialogsChanges(string groupName, int dialogId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(ChatAction.dialogsChanged.ToString(), await GetIdsResponce(dialogId));
        }

        public async Task UserStateUpdated(int userId, int userStateId)
        {
            await hubContext.Clients.Group($"direct{userId}").SendAsync(ChatAction.userStateUpdated.ToString(), userStateId);
        }
    }
}
