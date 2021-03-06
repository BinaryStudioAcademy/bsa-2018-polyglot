﻿using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.Common.Helpers.SignalR;
using System.Threading.Tasks;
using Polyglot.Core.Authentication;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRWorkspaceService : SignalRCommonService<WorkspaceHub>, ISignalRWorkspaceService
    {
        public SignalRWorkspaceService(IHubContext<WorkspaceHub> hubContext, ICurrentUser currentUser) : base(hubContext, currentUser)
        {
        }

        public async Task ChangedTranslation(string groupName, int translationId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.changedTranslation.ToString(), await GetIdsResponce(translationId));
        }

        public async Task СommentsChanged(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.commentsChanged.ToString(), await GetIdsResponce(complexStringId));
        }

        public async Task ComplexStringAdded(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.complexStringAdded.ToString(), await GetIdsResponce(complexStringId));
        }

        public async Task ComplexStringRemoved(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.complexStringRemoved.ToString(), await GetIdsResponce(complexStringId));
        }

        public async Task LanguageRemoved(string groupName, int languageId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.languageRemoved.ToString(), await GetIdsResponce(languageId));
        }

        public async Task LanguagesAdded(string groupName, int[] languagesIds)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.languagesAdded.ToString(), await GetIdsResponce(languagesIds));
        }

        public async Task LanguageTranslationCommitted(string groupName, int languageId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.languageTranslationCommitted.ToString(), await GetIdsResponce(languageId));
        }

        public async Task ComplexStringTranslatingStarted(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.complexStringTranslatingStarted.ToString(), await GetIdsResponce(complexStringId));
        }

        public async Task ComplexStringTranslatingFinished(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.complexStringTranslatingFinished.ToString(), await GetIdsResponce(complexStringId));
        }
    }
}
