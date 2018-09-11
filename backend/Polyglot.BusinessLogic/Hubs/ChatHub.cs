using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using System;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Hubs
{
    [Authorize]
    public class ChatHub : BaseHub
    {
        private readonly IChatService chatService;

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public override async Task JoinGroup(string groupName)
        {
            var c = Context;
            await base.JoinGroup(groupName);
        }

        public override Task LeaveGroup(string groupName)
        {
            var c = Context;
            return base.LeaveGroup(groupName);
        }

        public override async Task OnConnectedAsync()
        {
            var c = Context.ConnectionId;
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var c = Context;
            return base.OnDisconnectedAsync(exception);
        }

        public async Task MessageRead(string whose)
        {
            var c = Context;
            // отсылаем собеседнику 'whose' свой id, чтобы он знал что все его сообщения вы прочитали
            await Clients.Group(whose).SendAsync(ChatAction.messageRead.ToString(), 1111);
        }

        public async Task Typing()
        {
            await Clients.OthersInGroup("target group").SendAsync(ChatAction.typing.ToString(), "sender name");
        }
    }
}
