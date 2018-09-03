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

namespace Polyglot.BusinessLogic.Services
{
    public class ChatService : IChatService
    {
        private readonly IProjectService projectService;
        private readonly ITeamService teamService;
        private readonly IMapper mapper;

        public ChatService(IProjectService projectService,
            ITeamService teamService, IMapper mapper)
        {
            this.projectService = projectService;
            this.teamService = teamService;
            this.mapper = mapper;
        }

        public async Task<ChatContactsDTO> GetContactsAsync(ChatGroup targetGroup, int targetGroupItemId)
        {
            return null;
            //var currentUser = CurrentUser.GetCurrentUserProfile();
            //var user = await CurrentUser.GetCurrentUserProfile();
            //List<Project> result = new List<Project>();
            //if (user.UserRole == Role.Manager)
            //{
            //    result = await uow.GetRepository<Project>().GetAllAsync(x => x.UserProfile.Id == user.Id);
            //}
            //else
            //{
            //    var translatorTeams = await uow.GetRepository<TeamTranslator>().GetAllAsync(x => x.TranslatorId == user.Id);
            //
            //    translatorTeams.ForEach(team => team.Team.ProjectTeams.ToList()
            //        .ForEach(project => result.Add(project.Project)));
            //
            //    result = result.Distinct().ToList();
            //}
            //return mapper.Map<List<ProjectDTO>>(result);
            //return new ChatContactsDTO();
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
                case ChatGroup.User:
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
    }
}
