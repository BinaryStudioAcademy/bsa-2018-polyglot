using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService service;

        public NotificationsController(INotificationService service)
        {
            this.service = service;
        }

        // GET: Notifications
        [HttpGet]
        public async Task<IActionResult> GetCurrentuserNotifications()
        {
            var notifications = await service.GetNotificationsByUserId((await CurrentUser.GetCurrentUserProfile()).Id);
            return notifications == null ? NotFound("No notification found!") as IActionResult
                : Ok(notifications);
        }

        // POST: Notifications
        public async Task<IActionResult> SendNotification([FromBody]NotificationDTO notification)
        {
            var notifications = await service.SendNotification(notification);
            return notifications == null ? NotFound("No notification send!") as IActionResult
                : Ok(notifications);
        }
    }
}