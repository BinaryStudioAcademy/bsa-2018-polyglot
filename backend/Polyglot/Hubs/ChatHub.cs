using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polyglot.Hubs.Helpers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    [Authorize]
    public class ChatHub : BaseHub
    {
      //  private static readonly ConcurrentDictionary<string, User> Users
      //  = new ConcurrentDictionary<string, User>();

        public override async Task OnConnectedAsync()
        {

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            await Clients.Group(Group.chatShared.ToString()).SendAsync(Action.chatMessageReceived.ToString(), message);
        }
    }
}
