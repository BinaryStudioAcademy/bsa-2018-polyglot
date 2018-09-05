using Polyglot.Common.DTOs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignalRChatService
    {
        Task MessageReveived(string groupName, ChatMessageDTO message);
    }
}
