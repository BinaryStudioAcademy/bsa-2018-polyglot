using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    public interface INotificationService : ICRUDService<Notification, NotificationDTO>
    {
        Task<IEnumerable<NotificationDTO>> GetNotificationsByUserId(int userId);

        Task<NotificationDTO> SendNotification(NotificationDTO notification);
    }
}
