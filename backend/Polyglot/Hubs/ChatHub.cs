using Microsoft.AspNetCore.SignalR;
using Polyglot.DataAccess.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }

        public async Task NewComment(Comment[] comments)
        {
            await Clients.All.SendAsync("commentsReceived", comments);
        }
    }
}
