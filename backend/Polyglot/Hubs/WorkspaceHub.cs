using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Polyglot.Common.DTOs.NoSQL;

namespace Polyglot.Hubs
{
    public class WorkspaceHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            try
            {
                // ConnectionId может быть уже недоступен и по истечению time out
                // будет сгенерировано исключение
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            }
            catch (TaskCanceledException ex)
            {

            }
        }
    }
}


//public async Task NewComplexString(string projectId, int newStringId)
//{
//    await Clients.Group(projectId).SendAsync("stringAdded", newStringId);
//#warning заменить
//    //  await Clients.OthersInGroup(projectId).SendAsync("stringAdded", newStringId);
//}

//public async Task ComplexStringDeleted(string projectId, int deletedStingId)
//{
//    await Clients.Group(projectId).SendAsync("stringDeleted", deletedStingId);
//#warning заменить 
//    // await Clients.OthersInGroup(projectId).SendAsync("stringDeleted", deletedStingId);
//}

//public async Task NewLanguage(string projectId, int[] languageIds)
//{
//    await Clients.Group(projectId).SendAsync("languageAdded", languageIds);
//#warning заменить 
//    // await Clients.OthersInGroup(projectId).SendAsync("languageAdded", languageIds);
//}

//public async Task LanguageDeleted(string projectId, int languageId)
//{
//    await Clients.Group(projectId).SendAsync("languageDeleted", languageId);
//#warning заменить 
//    // await Clients.OthersInGroup(projectId).SendAsync("languageDeleted", languageId);
//}
