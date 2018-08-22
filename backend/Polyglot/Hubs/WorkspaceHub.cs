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

        public async Task NewComplexString(string projectId, int newStringId)
        {
            await Clients.Group(projectId).SendAsync("stringAdded", newStringId);
#warning заменить
          //  await Clients.OthersInGroup(projectId).SendAsync("stringAdded", newStringId);
        }

        public async Task ComplexStringDeleted(string projectId, int deletedStingId)
        {
            await Clients.Group(projectId).SendAsync("stringDeleted", deletedStingId);
#warning заменить 
            // await Clients.OthersInGroup(projectId).SendAsync("stringDeleted", deletedStingId);
        }

        public async Task NewLanguage(string projectId, int[] languageIds)
        {
            await Clients.Group(projectId).SendAsync("languageAdded", languageIds);
#warning заменить 
            // await Clients.OthersInGroup(projectId).SendAsync("languageAdded", languageIds);
        }

        public async Task LanguageDeleted(string projectId, int languageId)
        {
            await Clients.Group(projectId).SendAsync("languageDeleted", languageId);
#warning заменить 
            // await Clients.OthersInGroup(projectId).SendAsync("languageDeleted", languageId);
        }

        public async Task NewTranslation(string projectId, int complexStringId, int languageId)
        {
            await Clients.Group(projectId).SendAsync("stringTranslated", complexStringId, languageId);
#warning заменить
        //    await Clients.OthersInGroup(projectId).SendAsync("stringTranslated", complexStringId, languageId);
        }

        public async Task Translating(string projectId, int complexStringId, int languageId, int translatingById, string translatingByFullName)
        {
            await Clients.Group(projectId).SendAsync("stringTranslating", translatingById, translatingByFullName);
#warning заменить
           // await Clients.OthersInGroup(projectId).SendAsync("stringTranslating", translatingById, translatingByFullName);
        }
    }
}
