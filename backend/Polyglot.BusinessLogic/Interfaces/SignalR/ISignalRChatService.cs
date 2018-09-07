using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignalRChatService
    {
        Task MessageReveived(string groupName, int dialogId, int messageId, string messageText);
    }
}
