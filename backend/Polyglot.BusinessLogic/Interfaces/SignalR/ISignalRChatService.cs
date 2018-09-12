using Polyglot.Core.SignalR.Responses;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignalRChatService
    {
        // не работает из-за currentUser == null
     //   Task MessageReveived(string groupName, int dialogId, int messageId, string messageText);

        Task MessageReveived(string groupName, ChatMessageResponce data);
    }
}