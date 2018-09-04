using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Hubs
{
    public class ChatHub : BaseHub
    {
        public ChatHub()
        {

        }

        public override async Task JoinGroup(string groupName)
        {
            await base.JoinGroup(groupName);
        }

        public override async Task OnConnectedAsync()
        {
            var c = Context.ConnectionId;
            var b = Context.UserIdentifier;
            var bb = Context.User.GetName();
            await base.OnConnectedAsync();
        }
        
        public async Task MessageRead(string whose)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return;

            // отсылаем собеседнику 'whose' свой id, чтобы он знал что все его сообщения вы прочитали
            await Clients.Group(whose).SendAsync(ChatAction.messageRead.ToString(), currentUser.Id);
        }

        public async Task Typing()
        {
            await Clients.OthersInGroup("target group").SendAsync(ChatAction.typing.ToString(), "sender name");
        }
    }
}
