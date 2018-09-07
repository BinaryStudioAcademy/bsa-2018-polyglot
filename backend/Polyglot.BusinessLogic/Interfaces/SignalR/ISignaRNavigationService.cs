using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces.SignalR
{
    public interface ISignaRNavigationService
    {
        Task NotificationAdded(string groupName, int notificationId);
    }
}
