using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.DTOs.NoSQL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polyglot.Hubs.Helpers;

namespace Polyglot.Hubs
{
    public class SignalrWorkspaceService : SignalrCommonService<WorkspaceHub>, ISignalrWorkspaceService
    {
        public SignalrWorkspaceService(IHubContext<WorkspaceHub> hubContext) : base(hubContext)
        {
        }

        public async Task ChangedTranslation(string groupName, TranslationDTO entity)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.changedTranslation.ToString(), entity);
        }

        public async Task CommentAdded(string groupName, IEnumerable<CommentDTO> comment)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.commentAdded.ToString(), comment);
        }

        public async Task CommentDeleted(string groupName, IEnumerable<CommentDTO> comment)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.commentDeleted.ToString(), comment);
        }

        public async Task CommentEdited(string groupName, IEnumerable<CommentDTO> comment)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.commentEdited.ToString(), comment);
        }

        public async Task ComplexStringAdded(string groupName, int complexStringId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.complexStringAdded.ToString(), complexStringId);
        }

        public async Task ComplexStringRemoved(string groupName, int complexStringId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.complexStringRemoved.ToString(), complexStringId);
        }

        public async Task LanguageRemoved(string groupName, int languageId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.languageRemoved.ToString(), languageId);
        }

        public async Task LanguagesAdded(string groupName, int[] languagesIds)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.languagesAdded.ToString(), languagesIds);
        }

        public async Task LanguageTranslationCommitted(string groupName, int languageId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.languageTranslationCommitted.ToString(), languageId);
        }
    }
}
