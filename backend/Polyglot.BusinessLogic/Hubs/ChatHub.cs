using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.Helpers.SignalR;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Hubs
{
   // [Authorize]
    public class ChatHub : BaseHub
    {
      //  private static readonly ConcurrentDictionary<string, User> Users
      //  = new ConcurrentDictionary<string, User>();

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string groupName, string message)
        {

            await Clients.OthersInGroup(Group.chatShared.ToString()).SendAsync(ChatAction.messageReceived.ToString(), message);
        }

        public async Task MessageRead()
        {
            int userId = -1;
            int messageId = -1;
            await Clients.User(userId.ToString()).SendAsync(ChatAction.messageRead.ToString(), messageId);
        }

        public async Task Typing()
        {
            await Clients.OthersInGroup("target group").SendAsync(ChatAction.typing.ToString(), "sender name");
        }
    }
}
