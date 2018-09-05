using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IChatService
    {
        Task<ChatContactsDTO> GetContactsAsync(ChatGroup targetGroup, int targetGroupItemId);

        Task<ChatUserStateDTO> GetUserStateAsync(int userId);

        Task<IEnumerable<ChatUserStateDTO>> GetContactsStateAsync();

        Task<IEnumerable<ProjectDTO>> GetProjectsAsync();

        Task<IEnumerable<TeamPrevDTO>> GetTeamsAsync();

        Task<ChatMessageDTO> GetMessageAsync(int messageId);

        Task<ChatMessageDTO> SendMessage(ChatMessageDTO message, ChatGroup targetGroup, int targetGroupItemId);

        Task<IEnumerable<ChatMessageDTO>> GetGroupMessagesHistoryAsync(ChatGroup targetGroup, int targetGroupItemId);
        
    }
}
