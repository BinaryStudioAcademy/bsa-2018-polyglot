using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.BusinessLogic.Services.SignalR;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Microsoft.AspNetCore.Authorization;
using Polyglot.DataAccess.Entities.Chat;

namespace Polyglot.BusinessLogic.Services
{
    [Authorize]
    public class ChatService : IChatService
    {
        private readonly IProjectService projectService;
        private readonly ITeamService teamService;
        private readonly ISignalRChatService signalRChatService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public ChatService(
            ISignalRChatService signalRChatService,
            IProjectService projectService,
            ITeamService teamService, IMapper mapper, IUnitOfWork uow)
        {
            this.signalRChatService = signalRChatService;
            this.projectService = projectService;
            this.teamService = teamService;
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<ChatContactsDTO> GetContactsAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            ChatContactsDTO contacts = new ChatContactsDTO()
            {
                ChatUserId = targetGroupItemId
            };

            switch (targetGroup)
            {
                case ChatGroup.chatUser:
                    {
                        var c = await GetUserContacts(currentUser);
                        if(c != null)
                        {
                            contacts.ContactList = c;
                        }
                        break;
                    }
                case ChatGroup.Project:
                    break;
                case ChatGroup.Team:
                    break;
                default:
                    break;
            }
            return contacts;
        }

        public async Task<IEnumerable<ChatUserStateDTO>> GetContactsStateAsync()
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            return null;
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetGroupMessagesHistoryAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            IEnumerable<ChatMessage> messages = null;
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            switch (targetGroup)
            {
                case ChatGroup.chatUser:
                    {
                        messages = await uow.GetRepository<ChatMessage>()
                            .GetAllAsync(m => (m.RecipientId == currentUser.Id && m.SenderId == targetGroupItemId) || (m.RecipientId == targetGroupItemId && m.SenderId == currentUser.Id));
                        break;
                    }
                case ChatGroup.Project:
                    break;
                case ChatGroup.Team:
                    break;
                default:
                    break;
            }

            return mapper.Map<IEnumerable<ChatMessageDTO>>(messages);
        }

        public async Task<ChatMessageDTO> GetMessageAsync(int messageId)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            var message = await uow.GetRepository<ChatMessage>()
                            .GetAsync(m => (m.RecipientId == currentUser.Id || m.SenderId == currentUser.Id) && m.Id == messageId);

            return message != null ? mapper.Map<ChatMessageDTO>(message) : null;
        }

        public Task<ChatUserStateDTO> GetUserStateAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsAsync()
        {
            return await projectService.GetListAsync() ?? null; 
        }

        public async Task<IEnumerable<TeamPrevDTO>> GetTeamsAsync()
        {
            return await teamService.GetAllTeamsAsync() ?? null;
        }


        public async Task<ChatMessageDTO> SendMessage(ChatMessageDTO message, ChatGroup targetGroup, int targetGroupItemId)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null || message == null)
                return null;

            ChatMessage addedMessage = null;

            switch (targetGroup)
            {
                case ChatGroup.chatUser:
                    {
                        message.IsRead = false;
                        message.ReceivedDate = DateTime.Now;
                        message.SenderId = currentUser.Id;
                        addedMessage = await uow.GetRepository<ChatMessage>().CreateAsync(mapper.Map<ChatMessage>(message));
                        await signalRChatService.MessageReveived($"{targetGroup}{targetGroupItemId}", message);
                        break;
                    }
                    
                case ChatGroup.Project:
                    break;
                case ChatGroup.Team:
                    break;
                default:
                    break;
            }

            return (await uow.SaveAsync() > 0 && addedMessage != null) ? mapper.Map<ChatMessageDTO>(addedMessage) : null;
        }

        private async Task<IEnumerable<ChatUserDTO>> GetUserContacts(UserProfile currentUser)
        {
            IEnumerable<ChatUserDTO> contacts = null;
            List<UserProfile> users = null;
            if (currentUser.UserRole == Role.Manager)
            {
                users = (await uow.GetRepository<Project>().GetAllAsync(p => p.UserProfile.Id == currentUser.Id))
                    .SelectMany(p => p.ProjectTeams)
                    .Select(pt => pt.Team)
                    .SelectMany(t => t.TeamTranslators)
                    .Select(tt => tt.UserProfile)
                    .ToList();
            }
            else
            {
                var translatorTeams = (await uow.GetRepository<TeamTranslator>()
                    .GetAllAsync(x => x.TranslatorId == currentUser.Id))
                    .Select(tt => tt.Team);

                users = translatorTeams
                    .SelectMany(t => t.TeamTranslators)
                    .Select(tt => tt.UserProfile)
                    .Where(u => u.Id != currentUser.Id)
                    .ToList();

                users.AddRange(translatorTeams.Select(tt => tt.CreatedBy));
            }

            // достать из базы userState, последнее сообщение и примапить
            contacts = mapper.Map <List<UserProfile>, List<ChatUserDTO>>(users, opt => opt.AfterMap((src, dest) =>
            {
                List<ChatUserDTO> result = dest;
                result.ForEach(u => 
                {
                    u.LastMessageText = "Last message text from server, hell yeah!!1111";
                    u.LastSeen = DateTime.Now;
                    u.IsOnline = true;
                });
            }));

            return contacts;
        }
    }
}


//messages = new List<ChatMessageDTO>
//{
//    new ChatMessageDTO()
//    {
//        Id = 0,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = currentUser.Id,
//        SenderId = targetGroupItemId,
//        Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
//    },
//    new ChatMessageDTO()
//    {
//        Id = 1,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = currentUser.Id,
//        SenderId = targetGroupItemId,
//        Body = "If truth is beauty, how come no one has their hair done in the library?"
//    },
//    new ChatMessageDTO()
//    {
//        Id = 2,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = targetGroupItemId,
//        SenderId = currentUser.Id,
//        Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
//    },
//    new ChatMessageDTO()
//    {
//        Id = 3,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = currentUser.Id,
//        SenderId = targetGroupItemId,
//        Body = "Ignorance must certainly be bliss or there wouldn't be so many people so resolutely pursuing it.My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
//    },
//    new ChatMessageDTO()
//    {
//        Id = 4,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = currentUser.Id,
//        SenderId = targetGroupItemId,
//        Body = "the high school after high school!"
//    },
//    new ChatMessageDTO()
//    {
//        Id = 5,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = targetGroupItemId,
//        SenderId = currentUser.Id,
//        Body = "A professor is one who talks in someone else's sleep."
//    },
//    new ChatMessageDTO()
//    {
//        Id = 6,
//        IsRead = true,
//        ReceivedDate = DateTime.Now,
//        RecipientId = targetGroupItemId,
//        SenderId = currentUser.Id,
//        Body = "About all some men accomplish in life is to send a son to Harvard. My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
//    },new ChatMessageDTO()
//    {
//        Id = 7,
//        IsRead = false,
//        ReceivedDate = DateTime.Now,
//        RecipientId = currentUser.Id,
//        SenderId = targetGroupItemId,
//        Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
//    },
//    new ChatMessageDTO()
//    {
//        Id = 8,
//        IsRead = false,
//        ReceivedDate = DateTime.Now,
//        RecipientId = targetGroupItemId,
//        SenderId = currentUser.Id,
//        Body = "The world is coming to an end! Repent and return those library books!"
//    },
//    new ChatMessageDTO()
//    {
//        Id = 9,
//        IsRead = false,
//        ReceivedDate = DateTime.Now,
//        RecipientId = targetGroupItemId,
//        SenderId = currentUser.Id,
//        Body = "So, is the glass half empty, half full, or just twice as large as it needs to be?."
//    }
//};