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
        public async Task NewComplexString(ComplexStringDTO newString)
        {
            await Clients.All.SendAsync("stringAdded", newString);
        }

        public async Task ComplexStringDeleted(int deletedStingId)
        {
            await Clients.All.SendAsync("stringDeleted", deletedStingId);
        }

        public async Task NewLanguage(string message)
        {
            await Clients.All.SendAsync("languageAdded", message);
        }

        public async Task LanguageDeleted(string message)
        {
            await Clients.All.SendAsync("languageDeleted", message);
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
