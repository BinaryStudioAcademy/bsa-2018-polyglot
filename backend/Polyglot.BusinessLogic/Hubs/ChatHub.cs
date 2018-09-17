using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Hubs
{
    [Authorize]
    public class ChatHub : BaseHub
    {
        private readonly IChatService chatService;

        internal static Dictionary<string, int> ConnectedUsers { get; set; } = new Dictionary<string, int>();

        public ChatHub(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public override async Task JoinGroup(string groupName)
        {
            await base.JoinGroup(groupName);
        }

        public override Task LeaveGroup(string groupName)
        {
            return base.LeaveGroup(groupName);
        }

        public override async Task OnConnectedAsync()
        {

            int userDbId = await chatService.ChangeUserStatus(targetUserUid: Context.UserIdentifier, isOnline: true);
            ConnectedUsers.TryAdd(Context.UserIdentifier, userDbId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await chatService.ChangeUserStatus(targetUserUid: Context.UserIdentifier, isOnline: false);
            ConnectedUsers.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task MessageRead(int dialogId, string whose)
        {
            // отсылаем собеседнику 'whose' свой id, чтобы он знал что все его сообщения вы прочитали
            Clients.Group(whose).SendAsync(ChatAction.messageRead.ToString(), Context.UserIdentifier);

            await chatService.ReadMessages(dialogId, Context.UserIdentifier);
        }

        public async Task Typing()
        {
            await Clients.OthersInGroup("target group").SendAsync(ChatAction.typing.ToString(), "sender name");
        }
    }
}
