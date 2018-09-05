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

        //Task<IEnumerable<ProjectDTO>> GetProjectsAsync();

        //Task<IEnumerable<TeamPrevDTO>> GetTeamsAsync();

        //Task<ChatMessageDTO> GetMessageAsync(int messageId);

        Task<ChatMessageDTO> SendMessage(ChatMessageDTO message);

        Task<ChatMessageDTO> GetMessageAsync(int messageId);

    //    Task<IEnumerable<ChatMessageDTO>> GetGroupMessagesHistoryAsync(ChatGroup targetGroup, int targetGroupItemId);

    }
}
