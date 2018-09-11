using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polyglot.Core.Authentication;
using Polyglot.Core.SignalR.Responses;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    [Authorize]
    public class SignalRCommonService<THub> where THub : Hub
    {
        protected readonly IHubContext<THub> hubContext;

        public SignalRCommonService(IHubContext<THub> hubContext)
        {
            this.hubContext = hubContext;
        }

        protected async Task<EntitiesIdsResponce> GetIdsResponce(params int[] ids)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            return new EntitiesIdsResponce()
            {
                SenderId = currentUser.Id,
                SenderFullName = currentUser.FullName,
                Ids = ids
            };
        }
        
        protected async Task<ChatMessageResponce> GetChatMessageResponce(int dialogId, int messageId, string text)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (text.Length > 155)
                text = text.Substring(0, 150);

            return new ChatMessageResponce()
            {
                SenderId = currentUser.Id,
                SenderFullName = currentUser.FullName,
                DialogId = dialogId,
                MessageId = messageId,
                Text = text
            };
        }
    }
}
