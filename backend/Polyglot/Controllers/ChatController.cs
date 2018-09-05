using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.Chat;
using Polyglot.Common.Helpers;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly IProjectService projectService;
        private readonly ITeamService teamService;

        public ChatController(IChatService chatService, IProjectService projectService, ITeamService teamService)
        {
            this.chatService = chatService;
            this.projectService = projectService;
            this.teamService = teamService;
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

        // GET: /chat/users/:userId/state
        //[HttpGet("users/{id}/state")]
        //public async Task<IActionResult> GetUserState(int userId)
        //{
        //    var userState = await chatService.GetUserStateAsync(userId);
        //    return userState == null ? NotFound("User not found!") as IActionResult
        //        : Ok(userState);
        //}

        //// GET: /chat/users/state
        //[HttpGet("users/state")]
        //public async Task<IActionResult> GetContactsUsersState()
        //{
        //    var userState = await chatService.GetUsersStateAsync();
        //    return userState == null ? NotFound("Users not found!") as IActionResult
        //        : Ok(userState);
        //}

        // GET: /chat/projects
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var contacts = await chatService.GetDialogsAsync(DataAccess.Helpers.ChatGroup.chatProject);
            return contacts == null ? NotFound("No dialogs found!") as IActionResult
                : Ok(contacts);

         //  var projects = await projectService.GetListAsync();
         //  return projects == null ? NotFound("No projects found!") as IActionResult
         //      : Ok(projects);
        }

        // GET: /chat/projects/:projectId/contacts
        //[HttpGet("projects/{id}/contacts")]
        //public async Task<IActionResult> GetProjectContacts(int projectId)
        //{
        //var contacts = await chatService.GetContactsAsync(ChatGroup.Project, projectId);
        //return contacts == null ? NotFound("No contacts found!") as IActionResult
        //: Ok(contacts);
        //}

        // GET: /chat/teams
        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var contacts = await teamService.GetListAsync();
            return contacts == null ? NotFound("No teams found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/teams/:teamId/contacts
        //[HttpGet("teams/{teamId}/contacts")]
        //public async Task<IActionResult> GetTeamContacts(int teamId)
        //{
        //var contacts = await chatService.GetContactsAsync(ChatGroup.Team, teamId);
        //return contacts == null ? NotFound("No contacts found!") as IActionResult
        //: Ok(contacts);
        //}

        //GET: /chat/messages/{:id}
        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await chatService.GetMessageAsync(id);
            return message == null ? NotFound("Message not found!") as IActionResult
                : Ok(message);
        }




        //// GET: /chat/projects/:projectId/messages
        //[HttpGet("projects/{projectId}/messages")]
        //public async Task<IActionResult> GetProjectMessagesHistory(int projectId)
        //{
        //    var messages = await chatService.GetGroupMessagesHistoryAsync(ChatGroup.Project, projectId);
        //    return messages == null ? NotFound("No messages found!") as IActionResult
        //        : Ok(messages);
        //}

        //// GET: /chat/teams/:teamId/messages
        //[HttpGet("teams/{teamId}/messages")]
        //public async Task<IActionResult> GetTeamMessagesHistory(int teamId)
        //{
        //    var messages = await chatService.GetGroupMessagesHistoryAsync(ChatGroup.Team, teamId);
        //    return messages == null ? NotFound("No messages found!") as IActionResult
        //        : Ok(messages);
        //}

        //POST: /chat/users/dialogs/messages
        [HttpPost("users/dialogs/messages")]
        public async Task<IActionResult> Post([FromBody]ChatMessageDTO message)
        {
            var m = await chatService.SendMessage(message);
            return m == null ? StatusCode(409) as IActionResult
                : Ok(m);
        }

        //  // POST: api/Chat
        //  [HttpPost]
        //  public void Post([FromBody] string value)
        //  {
        //  }
        //
        //  // PUT: api/Chat/5
        //  [HttpPut("{id}")]
        //  public void Put(int id, [FromBody] string value)
        //  {
        //  }
        //
        //  // DELETE: api/ApiWithActions/5
        //  [HttpDelete("{id}")]
        //  public void Delete(int id)
        //  {
        //  }
    }
}
