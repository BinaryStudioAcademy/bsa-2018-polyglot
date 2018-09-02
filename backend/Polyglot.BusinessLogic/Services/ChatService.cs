using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly ICRUDService<Project, ProjectDTO> projectService;
        private readonly IMapper mapper;

        public ChatService()
        {

        }

        public Task<ChatContactsDTO> GetContactsAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatUserStateDTO>> GetContactsStateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChatMessageDTO>> GetGroupMessagesHistoryAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatMessageDTO> GetMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectDTO>> GetProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamPrevDTO>> GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ChatUserStateDTO> GetUserStateAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
