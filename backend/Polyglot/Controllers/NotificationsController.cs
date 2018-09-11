using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService service;
        private readonly ICurrentUser _currentUser;

        public NotificationsController(INotificationService service, ICurrentUser currentUser)
        {
            this.service = service;
            _currentUser = currentUser;
        }

        // GET: Notifications
        [HttpGet]
        public async Task<IActionResult> GetCurrentuserNotifications()
        {
            var notifications = await service.GetNotificationsByUserId((await _currentUser.GetCurrentUserProfile()).Id);
            return notifications == null ? NotFound("No notification found!") as IActionResult
                : Ok(notifications);
        }

        // POST: Notifications
        public async Task<IActionResult> SendNotification([FromBody]NotificationDTO notification)
        {
            notification.SenderId = (await _currentUser.GetCurrentUserProfile()).Id;
            var entity = await service.SendNotification(notification);
            return entity == null ? StatusCode(409) as IActionResult
                : Ok(entity);
        }

        // Ву: Notifications
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            bool isDeleted = await service.TryDeleteAsync(id);
            return isDeleted ? Ok(await service.GetNotificationsByUserId((await _currentUser.GetCurrentUserProfile()).Id))
                : StatusCode(304) as IActionResult;
        }
    }
}