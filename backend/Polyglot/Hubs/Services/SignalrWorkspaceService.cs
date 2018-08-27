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

        public async Task CommentAdded(string groupName, IEnumerable<CommentDTO> comments)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.commentAdded.ToString(), comments);
        }

        public async Task ComplexStringAdded(string groupName, int complexStringId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.complexStringAdded.ToString(), complexStringId);
        }

        public async Task ComplexStringRemoved(string groupName, int complexStringId)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(Action.complexStringRemoved.ToString(), complexStringId);
        }
    }
}
