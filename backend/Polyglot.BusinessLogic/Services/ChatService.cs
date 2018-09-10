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



        public async Task<IEnumerable<ChatDialogDTO>> GetDialogsAsync(ChatGroup targetGroup)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            var dialogs = await uow.GetRepository<ChatDialog>()
                .GetAllAsync(d => d.DialogType == targetGroup && d.DialogParticipants.Select(dp => dp.Participant).Contains(currentUser));

            if (dialogs == null || dialogs.Count < 1)
                return null;

            List<ChatDialogDTO> result = null;

            switch (targetGroup)
            {
                case ChatGroup.direct:
                case ChatGroup.dialog:
                    result = mapper.Map<List<ChatDialog>, List<ChatDialogDTO>>(dialogs, opt => opt.AfterMap((src, dest) => FormUserDialogs(src, dest, currentUser)));
                    break;
                case ChatGroup.chatProject:
                case ChatGroup.chatTeam:
                    result = mapper.Map<List<ChatDialog>, List<ChatDialogDTO>>(dialogs, opt => opt.AfterMap((src, dest) => FormGroupDialogs(src, dest, currentUser)));
                    break;
                default:
                    break;
            }

            return result;
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetDialogMessagesAsync(ChatGroup targetGroup, int targetGroupDialogId)
        {
            if (targetGroupDialogId < 0)
                return null;

            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            IEnumerable<ChatMessage> messages = null;
            long dialogIdentifer = -1;

            switch (targetGroup)
            {
                case ChatGroup.dialog:
                case ChatGroup.direct:
                    {
                        dialogIdentifer = currentUser.Id + targetGroupDialogId;
                        break;
                    }
                case ChatGroup.chatProject:
                case ChatGroup.chatTeam:
                    {
                        dialogIdentifer = targetGroupDialogId;
                        break;
                    }
                default:
                    break;
            }

            messages = (await uow.GetRepository<ChatDialog>()
                .GetAsync(d => d.Identifier == dialogIdentifer && d.DialogType == targetGroup))
                ?.Messages;

            return messages != null ? mapper.Map<IEnumerable<ChatMessageDTO>>(messages) : null;
        }

        public async Task<ChatMessageDTO> SendMessage(ChatMessageDTO message)
        {
            var targetDialog = await uow.GetRepository<ChatDialog>().GetAsync(message.DialogId);
            if (targetDialog == null)
                return null;

            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null || message == null)
                return null;

            var clientMessageId = message.ClientId;
            message.ReceivedDate = DateTime.Now;
            message.SenderId = currentUser.Id;

            var a = mapper.Map<ChatMessage>(message);

            var newMessage = await uow.GetRepository<ChatMessage>().CreateAsync(a);
            await uow.SaveAsync();

            if (newMessage == null)
                return null;

            var dialogId = newMessage.DialogId.Value;
            var text = newMessage.Body;
            // отправляем уведомление о сообщении в диалог
            await signalRChatService.MessageReveived($"{targetDialog.DialogType.ToString()}{dialogId}", dialogId, newMessage.Id, text);

            //отправляем уведомление каждому участнику диалога
            foreach (var participant in targetDialog.DialogParticipants)
            {
                await signalRChatService.MessageReveived($"{ChatGroup.direct.ToString()}{participant.ParticipantId}", dialogId, newMessage.Id, text);
            }

            return mapper.Map<ChatMessageDTO>(newMessage, opt => opt.AfterMap((src, dest) => ((ChatMessageDTO)dest).ClientId = clientMessageId));
        }

        public async Task<IEnumerable<ChatUserStateDTO>> GetUsersStateAsync()
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            return null;
        }

        public async Task<ChatMessageDTO> GetMessageAsync(int messageId)
        {
            var currentUser = await CurrentUser.GetCurrentUserProfile();
            if (currentUser == null)
                return null;

            var message = await uow.GetRepository<ChatMessage>()
                            .GetAsync(messageId);

            if (message == null)
                return null;

            // current user должен принимать участие в диалоге к которому относится искомое сообщение 
            var targetDialog = message.Dialog
                ?.DialogParticipants
                ?.Select(dp => dp.Participant)
                ?.Where(p => p.Id == currentUser.Id)
                ?.FirstOrDefault();

            return targetDialog != null ? mapper.Map<ChatMessageDTO>(message) : null;
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

        public async Task<ChatDialogDTO> CreateDialog(ChatDialogDTO dialog)
        {
            if (dialog.Participants.Count() < 1)
                return null;

            var currentUser = await CurrentUser.GetCurrentUserProfile();

            if (currentUser == null)
                return null;

            var targetIdentifier = dialog.Participants.Sum(p => p.Id) + currentUser.Id;
            var d = await uow.GetRepository<ChatDialog>()
                .GetAsync(dd => dd.Identifier == targetIdentifier && dd.DialogType == ChatGroup.dialog);

            if (d == null)
            {
                var repo = uow.GetRepository<UserProfile>();
                UserProfile currentInterlocator;
                List<DialogParticipant> dp = new List<DialogParticipant>();

                foreach (var p in dialog.Participants)
                {
                    currentInterlocator = await repo.GetAsync(p.Id);
                    if (currentInterlocator != null)
                    {
                        dp.Add(new DialogParticipant() { Participant = currentInterlocator });
                    }
                }
                dp.Add(new DialogParticipant() { Participant = currentUser });

                if (dp.Count < 2)
                {
                    return null;
                }


                var newDialog = await uow.GetRepository<ChatDialog>()
                    .CreateAsync(new ChatDialog()
                    {
                        Identifier = targetIdentifier,
                        DialogParticipants = dp,
                        DialogType = ChatGroup.dialog
                    }
                    );

                await uow.SaveAsync();
                return mapper.Map<ChatDialogDTO>(newDialog);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteDialog(int id)
        {
            await uow.GetRepository<ChatDialog>().DeleteAsync(id);
            return await uow.SaveAsync() > 0;
        }

        #region Private members

        private void FormGroupDialogs(List<ChatDialog> src, List<ChatDialogDTO> dest, UserProfile currentUser)
        {
            List<ChatDialogDTO> result = dest;
            result.ForEach(d =>
            {
                var accordingSourceDialog = src.Find(dialog => dialog.Id == d.Id);
                d.UnreadMessagesCount = accordingSourceDialog.Messages
                        .Where(m => m.SenderId != currentUser.Id && !m.IsRead)
                        .Count();
            });
        }

        private void FormUserDialogs(List<ChatDialog> src, List<ChatDialogDTO> dest, UserProfile currentUser)
        {
            List<ChatDialogDTO> result = dest;
            string lastMessage;
            result.ForEach(d =>
            {
                var accordingSourceDialog = src.Find(dialog => dialog.Id == d.Id);
                var interlocator = accordingSourceDialog
                    .DialogParticipants
                    .Select(dp => dp.Participant)
                    .Where(p => p.Id != currentUser.Id)
                    .FirstOrDefault();

                if (interlocator != null)
                {
                    lastMessage = accordingSourceDialog
                        .Messages.LastOrDefault(m => m.SenderId == interlocator.Id)
                        ?.Body;
                    d.LastMessageText = lastMessage != null ? lastMessage : ""; 

                    if (d.LastMessageText.Length > 155)
                    {
                        d.LastMessageText = d.LastMessageText.Substring(0, 150);
                    }

                    d.UnreadMessagesCount = accordingSourceDialog.Messages
                        .Where(m => m.SenderId == interlocator.Id && !m.IsRead)
                        .Count();

                    d.Participants = new List<ChatUserDTO>()
                    {
                        mapper.Map<ChatUserDTO>(interlocator)
                    };
                }
            });
        }

        #endregion Private members
    }
}