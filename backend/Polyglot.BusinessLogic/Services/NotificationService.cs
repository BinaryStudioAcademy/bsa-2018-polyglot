using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Polyglot.Common.DTOs;
using Polyglot.Common.Helpers.SignalR;
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
        private readonly ISignaRNavigationService signaRNavigationService;
        public NotificationService(IUnitOfWork uow, IMapper mapper, IUserService userService, ISignaRNavigationService signaRNavigationService) : base(uow, mapper)
        {
            this.userService = userService;
            this.signaRNavigationService = signaRNavigationService;
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
            if (notification != null)
            {
                await signaRNavigationService.NotificationAdded($"{Group.notification}{notification.ReceiverId}", notification.Id);
                return mapper.Map<NotificationDTO>(notification);
            }
            return null;
        }
        public override async Task<bool> TryDeleteAsync(int identifier)
        {
            if (uow != null)
            {
                await uow.GetRepository<Option>().GetAllAsync(o => o.NotificationId == identifier);
                var notification = await uow.GetRepository<Notification>().GetAsync(identifier);
                notification.Options = null;
                notification = await uow.GetRepository<Notification>().Update(notification);
                await uow.GetRepository<Notification>().DeleteAsync(identifier);
                await uow.SaveAsync();
                return true;
            }
            else
                return false;
        }
    }
}
