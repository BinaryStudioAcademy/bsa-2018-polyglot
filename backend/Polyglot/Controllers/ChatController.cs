using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers;
using Polyglot.Core.Authentication;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly ICurrentUser currentUser;

        public ChatController(IChatService chatService, ICurrentUser currentUser)
        {
            this.chatService = chatService;
            this.currentUser = currentUser;
        }

        // GET: /chat/dialogs
        [HttpGet("dialogs")]
        public async Task<IActionResult> GetDialogs()
        {
            var contacts = await chatService.GetDialogsAsync(DataAccess.Helpers.ChatGroup.dialog);
            return contacts == null ? NotFound("No dialogs found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/dialogs/users/{targetGroupDialogId}/messages
        [HttpGet("dialogs/users/{targetGroupDialogId}/messages")]
        public async Task<IActionResult> GetDialogMessages(int targetGroupDialogId)
        {
            var messages = await chatService.GetDialogMessagesAsync(DataAccess.Helpers.ChatGroup.dialog, targetGroupDialogId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
        }

        // GET: /chat/dialogs/projects/{targetGroupDialogId}/messages
        [HttpGet("dialogs/projects/{targetGroupDialogId}/messages")]
        public async Task<IActionResult> GetProjectMessages(int targetGroupDialogId)
        {
            var messages = await chatService.GetDialogMessagesAsync(DataAccess.Helpers.ChatGroup.chatProject, targetGroupDialogId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
        }

        // GET: /chat/dialogs/teams/{targetGroupDialogId}/messages
        [HttpGet("dialogs/teams/{targetGroupDialogId}/messages")]
        public async Task<IActionResult> GetTeamMessages(int targetGroupDialogId)
        {
            var messages = await chatService.GetDialogMessagesAsync(DataAccess.Helpers.ChatGroup.chatTeam, targetGroupDialogId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
        }

        // GET: /chat/projects
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await chatService.GetDialogsAsync(DataAccess.Helpers.ChatGroup.chatProject);
            return projects == null ? NotFound("No projects found!") as IActionResult
                : Ok(projects);
        }

        // GET: /chat/teams
        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await chatService.GetDialogsAsync(DataAccess.Helpers.ChatGroup.chatTeam);
            return teams == null ? NotFound("No teams found!") as IActionResult
                : Ok(teams);
        }
        
        //GET: /chat/messages/{:id}
        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await chatService.GetMessageAsync(id);
            return message == null ? NotFound("Message not found!") as IActionResult
                : Ok(message);
        }

        //POST: /chat/users/dialogs/messages
        [HttpPost("users/dialogs/messages")]
        public async Task<IActionResult> Post([FromBody]ChatMessageDTO message)
        {
            var m = await chatService.SendMessage(message);
            return m == null ? StatusCode(409) as IActionResult
                : Ok(m);
        }

        //POST: /chat/dialogs
        [HttpPost("dialogs")]
        public async Task<IActionResult> CreateDialog([FromBody]ChatDialogDTO dialog)
        {
            var d = await chatService.CreateDialog(dialog);
            return d == null ? StatusCode(409) as IActionResult
                : Ok(d);
        }

        //DELETE: /chat/dialogs/:id
        [HttpDelete("dialogs/{id}")]
        public async Task<IActionResult> DeleteDialog(int id)
        {
            var success = await chatService.DeleteDialog(id);
            return success == null ? StatusCode(409) as IActionResult
                : Ok(success);
        }

        //POST: /chat/startDialog
        [HttpPost("startDialog")]
        public async Task<IActionResult> StartChatWithUser([FromBody]UserProfileDTO user)
        {
            var d = await chatService.StartChatWithUser(user);
            return d == null ? StatusCode(409) as IActionResult
                : Ok(d);
        }

        //POST: /chat/getNumberOfUnread
        [HttpGet("getNumberOfUnread")]
        public async Task<IActionResult> GetNumberOfUnread()
        {
            var currentUserId = (await this.currentUser.GetCurrentUserProfile()).Id;
            int unreadMessages = await chatService.GetNumberOfUnreadMessages(currentUserId);
            return Ok(unreadMessages);
        }
    }
}
