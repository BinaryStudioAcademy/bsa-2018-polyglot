using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.DTOs.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    public class SignalrWorkspaceService : SignalrCommonService<WorkspaceHub>, ISignalrWorkspaceService
    {
        public SignalrWorkspaceService(IHubContext<WorkspaceHub> hubContext) : base(hubContext)
        {
        }

        public async Task ChangedTranslation(string groupName, TranslationDTO entity)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(SignalrSubscribeActions.changedTranslation.ToString(), entity);
        }

        public async Task CommentAdded(string groupName, IEnumerable<CommentDTO> comments)
        {
            await _hubContext.Clients.Group(groupName).SendAsync(SignalrSubscribeActions.commentAdded.ToString(), comments);
        }
    }
}
