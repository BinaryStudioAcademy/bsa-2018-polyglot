using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyglot.Hubs
{
    public class SignalrCommonService<THub> where THub : Hub
    {
        protected readonly IHubContext<THub> _hubContext;

        public SignalrCommonService(IHubContext<THub> hubContext)
        {
            _hubContext = hubContext;
        }
    }
}
