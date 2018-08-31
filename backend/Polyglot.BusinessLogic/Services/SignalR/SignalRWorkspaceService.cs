using Microsoft.AspNetCore.SignalR;
using Polyglot.BusinessLogic.Hubs;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.Helpers.SignalR;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services.SignalR
{
    public class SignalRWorkspaceService : SignalRCommonService<WorkspaceHub>, ISignalRWorkspaceService
    {
        public SignalRWorkspaceService(IHubContext<WorkspaceHub> hubContext) : base(hubContext)
        {
        }

        public async Task ChangedTranslation(string groupName, int translationId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.changedTranslation.ToString(), await GetIdsResponce(translationId));
        }

        public async Task CommentAdded(string groupName, int complexStringId)
        {
            await hubContext.Clients.Group(groupName).SendAsync(Action.commentAdded.ToString(), await GetIdsResponce(complexStringId));
        }

        public Task CommentDeleted(string groupName, int commentId)
        {
#warning реализовать !!!!!1111111111
            throw new System.NotImplementedException();
        }

        public Task CommentEdited(string groupName, int commentId)
        {
#warning реализовать !!!!!1111111111
            throw new System.NotImplementedException();
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
    }
}
