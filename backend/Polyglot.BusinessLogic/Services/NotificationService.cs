using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    public class NotificationService : CRUDService<Notification, NotificationDTO>, INotificationService
    {
        private readonly IUserService userService;
        public NotificationService(IUnitOfWork uow, IMapper mapper, IUserService userService) : base(uow, mapper)
        {
            this.userService = userService;
        }
        public async Task<IEnumerable<NotificationDTO>> GetNotificationsByUserId(int userId)
        {
            var notifications =  await uow.GetRepository<Notification>().GetAllAsync(u => u.ReceiverId == userId);
            return notifications != null ? mapper.Map<List<NotificationDTO>>(notifications) : null;
        }

        public async Task<NotificationDTO> SendNotification(NotificationDTO notificationDTO)
        {
            var notification = await uow.GetRepository<Notification>().CreateAsync(mapper.Map<Notification>(notificationDTO));

            await uow.SaveAsync();
            return notification != null ? mapper.Map<NotificationDTO>(notification) : null;
        }
    }
}
