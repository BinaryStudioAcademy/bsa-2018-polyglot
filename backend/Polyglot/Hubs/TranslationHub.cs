using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Polyglot.Hubs
{
    public class TranslationHub : Hub
    {
        public async Task NewComplexString(string message)
        {
            await Clients.All.SendAsync("stringAdded", message);
        }

        public async Task NewLanguage(string message)
        {
            await Clients.All.SendAsync("languageAdded", message);
        }

        public async Task NewTranslation(string message)
        {
            await Clients.All.SendAsync("stringTranslated", message);
        }

        public async Task TranslationCommit(string message)
        {
            await Clients.All.SendAsync("stringTranslating", message);
        }
    }
}
