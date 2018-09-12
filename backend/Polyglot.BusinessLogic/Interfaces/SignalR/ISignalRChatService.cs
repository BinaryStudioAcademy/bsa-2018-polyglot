using Polyglot.Core.SignalR.Responses;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignalRChatService
    {
        Task DialogsChanges(string groupName, int dialogId);
        // не работает из-за currentUser == null
        //Task MessageReveived(string groupName, int dialogId, int messageId, string messageText);
        Task MessageReveived(string groupName, ChatMessageResponce data);
    }
}