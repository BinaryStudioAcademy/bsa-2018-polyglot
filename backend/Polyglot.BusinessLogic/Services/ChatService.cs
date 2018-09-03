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

namespace Polyglot.BusinessLogic.Services
{
    public class ChatService : CRUDService<Project, ProjectDTO>, IChatService
    {
        private readonly IProjectService projectService;
        private readonly ITeamService teamService;
        private readonly ISignalRChatService signalRChatService;

        public ChatService(
            ISignalRChatService signalRChatService,
            IProjectService projectService,
            ITeamService teamService, IMapper mapper, IUnitOfWork uow)
            :base(uow, mapper)
        {
            this.signalRChatService = signalRChatService;
            this.projectService = projectService;
            this.teamService = teamService;
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

        public Task<IEnumerable<ChatUserStateDTO>> GetContactsStateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetGroupMessagesHistoryAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            List<ChatMessageDTO> messages = new List<ChatMessageDTO>();
            var currentUser = CurrentUser.GetCurrentUserProfile();
            switch (targetGroup)
            {
                case ChatGroup.chatUser:
                    {
                        messages = new List<ChatMessageDTO>
                        {
                            new ChatMessageDTO()
                            {
                                Id = 0,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
                            },
                            new ChatMessageDTO()
                            {
                                Id = 1,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "If truth is beauty, how come no one has their hair done in the library?"
                            },
                            new ChatMessageDTO()
                            {
                                Id = 2,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = 1,
                                SenderId = currentUser.Id,
                                Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
                            },
                            new ChatMessageDTO()
                            {
                                Id = 3,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "Ignorance must certainly be bliss or there wouldn't be so many people so resolutely pursuing it.My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
                            },
                            new ChatMessageDTO()
                            {
                                Id = 4,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "the high school after high school!"
                            },
                            new ChatMessageDTO()
                            {
                                Id = 5,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = 1,
                                SenderId = currentUser.Id,
                                Body = "A professor is one who talks in someone else's sleep."
                            },
                            new ChatMessageDTO()
                            {
                                Id = 6,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = 1,
                                SenderId = currentUser.Id,
                                Body = "About all some men accomplish in life is to send a son to Harvard. My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
                            },new ChatMessageDTO()
                            {
                                Id = 7,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "My father, a good man, told me, 'Never lose your ignorance; you cannot replace it."
                            },
                            new ChatMessageDTO()
                            {
                                Id = 8,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = currentUser.Id,
                                SenderId = 1,
                                Body = "The world is coming to an end! Repent and return those library books!"
                            },
                            new ChatMessageDTO()
                            {
                                Id = 9,
                                IsRead = true,
                                ReceivedDate = DateTime.Now,
                                RecipientId = 1,
                                SenderId = currentUser.Id,
                                Body = "So, is the glass half empty, half full, or just twice as large as it needs to be?."
                            }
                        };
                        break;
                    }
                case ChatGroup.Project:
                    break;
                case ChatGroup.Team:
                    break;
                default:
                    break;
            }
            return messages;
        }

        public Task<ChatMessageDTO> GetMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsAsync()
        {
            return await projectService.GetListAsync();
        }

        public async Task<IEnumerable<TeamPrevDTO>> GetTeamsAsync()
        {
            return await teamService.GetAllTeamsAsync();
        }

        public Task<ChatUserStateDTO> GetUserStateAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatMessageDTO> SendMessage(ChatMessageDTO message, ChatGroup targetGroup, int targetGroupItemId)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            switch (targetGroup)
            {
                case ChatGroup.chatUser:
                    {
                        message.IsRead = true;
                        message.ReceivedDate = DateTime.Now;
                        message.SenderId = currentUser.Id;
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
            return message;
        }

        private async Task<IEnumerable<ChatUserDTO>> GetUserContacts(UserProfile currentUser)
        {
            IEnumerable<ChatUserDTO> contacts = null;
            if (currentUser.UserRole == Role.Manager)
            {
                var users = (await uow.GetRepository<Project>().GetAllAsync(p => p.UserProfile.Id == currentUser.Id))
                    .SelectMany(p => p.ProjectTeams)
                    .Select(pt => pt.Team)
                    .SelectMany(t => t.TeamTranslators)
                    .Select(tt => tt.UserProfile);

                contacts = mapper.Map<IEnumerable<ChatUserDTO>>(users);
            }
            else
            {
                var translatorTeams = (await uow.GetRepository<TeamTranslator>()
                    .GetAllAsync(x => x.TranslatorId == currentUser.Id))
                    .Select(tt => tt.Team);

                var users = translatorTeams
                    .SelectMany(t => t.TeamTranslators)
                    .Select(tt => tt.UserProfile).Where(u => u.Id != currentUser.Id).ToList();

                users.AddRange(translatorTeams.Select(tt => tt.CreatedBy));

                contacts = mapper.Map<IEnumerable<ChatUserDTO>>(users);
            }

            return contacts;
        }
    }
}
