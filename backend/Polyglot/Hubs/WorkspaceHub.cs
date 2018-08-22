using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.DTOs.NoSQL;

namespace Polyglot.Hubs
{
    public class WorkspaceHub : Hub
    {
        public async Task JoinProjectGroup(string projectId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
        }

        public async Task LeaveProjectGroup(string projectId)
        {
            try
            {
                // ConnectionId может быть уже недоступен и по истечению time out
                // будет сгенерировано исключение
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);
            }catch(TaskCanceledException ex)
            {

            }
        }

        public async Task NewComplexString(ComplexStringDTO newString)
        {
            await Clients.All.SendAsync("stringAdded", newString);
        }

        public async Task ComplexStringDeleted(int deletedStingId)
        {
            await Clients.All.SendAsync("stringDeleted", deletedStingId);
        }

        public async Task NewLanguage(string projectId, string message)
        {
            await Clients.Group(projectId).SendAsync("languageAdded", message);
#warning заменить 
           // await Clients.OthersInGroup(projectId).SendAsync("languageAdded", message);
        }

        public async Task LanguageDeleted(string projectId, string message)
        {
            await Clients.Group(projectId).SendAsync("languageDeleted", message);
#warning заменить 
            // await Clients.OthersInGroup(projectId).SendAsync("languageDeleted", message);
        }

        public async Task NewTranslation(string message)
        {
            await Clients.All.SendAsync("stringTranslated", message);
        }

        public async Task Translating(int translatingById, string translatingByFullName)
        {
            await Clients.All.SendAsync("stringTranslating", translatingById, translatingByFullName);
        }
    }
}
