using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.DataAccess.Helpers;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<ChatDialogDTO>> GetDialogsAsync(ChatGroup targetGroup);

        Task<IEnumerable<ChatMessageDTO>> GetDialogMessagesAsync(ChatGroup targetGroup, int targetGroupDialogId);

      //  Task<ChatUserStateDTO> GetUserStateAsync(int userId);

      //  Task<IEnumerable<ChatUserStateDTO>> GetUsersStateAsync();
      
        Task<ChatDialogDTO> CreateDialog(ChatDialogDTO dialog);

        Task<bool> DeleteDialog(int id);

        Task<ChatMessageDTO> SendMessage(ChatMessageDTO message);

        Task<ChatMessageDTO> GetMessageAsync(int messageId);

        Task<ChatDialogDTO> StartChatWithUser(UserProfileDTO user);

        Task ReadMessages(int dialogId, string whoUid);

        Task ChangeUserStatus(string targetUserUid, bool isOnline);
    }
}
