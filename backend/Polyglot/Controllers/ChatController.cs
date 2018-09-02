using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.Helpers;

namespace Polyglot.Controllers
{
   // [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        // GET: /chat/users/:userId/contacts
        [HttpGet("users/{id}/contacts")]
        public async Task<IActionResult> GetUserContacts(int userId)
        {
            var contacts = await chatService.GetContactsAsync(ChatGroup.User, userId);
            return contacts == null ? NotFound("No contacts found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/users/:userId/state
        [HttpGet("users/{id}/state")]
        public async Task<IActionResult> GetUserState(int userId)
        {
            var userState = await chatService.GetUserStateAsync(userId);
            return userState == null ? NotFound("User not found!") as IActionResult
                : Ok(userState);
        }

        // GET: /chat/users/state
        [HttpGet("users/state")]
        public async Task<IActionResult> GetContactsUsersState()
        {
            var userState = await chatService.GetContactsStateAsync();
            return userState == null ? NotFound("Users not found!") as IActionResult
                : Ok(userState);
        }

        // GET: /chat/projects
        [HttpGet("projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await chatService.GetProjectsAsync();
            return projects == null ? NotFound("No projects found!") as IActionResult
                : Ok(projects);
        }

        // GET: /chat/projects/:projectId/contacts
        [HttpGet("projects/{id}/contacts")]
        public async Task<IActionResult> GetProjectContacts(int projectId)
        {
            var contacts = await chatService.GetContactsAsync(ChatGroup.Project, projectId);
            return contacts == null ? NotFound("No contacts found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/teams
        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var contacts = await chatService.GetTeamsAsync();
            return contacts == null ? NotFound("No teams found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/teams/:teamId/contacts
        [HttpGet("teams/{teamId}/contacts")]
        public async Task<IActionResult> GetTeamContacts(int teamId)
        {
            var contacts = await chatService.GetContactsAsync(ChatGroup.Team, teamId);
            return contacts == null ? NotFound("No contacts found!") as IActionResult
                : Ok(contacts);
        }

        // GET: /chat/messages
        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await chatService.GetMessageAsync(id);
            return message == null ? NotFound("Message not found!") as IActionResult
                : Ok(message);
        }


        // GET: /chat/users/:userId/messages
        [HttpGet("users/{userId}/messages")]
        public async Task<IActionResult> GetUserMessagesHistory(int userId)
        {
            var messages = await chatService.GetGroupMessagesHistoryAsync(ChatGroup.User, userId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
        }

        // GET: /chat/projects/:projectId/messages
        [HttpGet("projects/{projectId}/messages")]
        public async Task<IActionResult> GetProjectMessagesHistory(int projectId)
        {
            var messages = await chatService.GetGroupMessagesHistoryAsync(ChatGroup.Project, projectId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
        }

        // GET: /chat/teams/:teamId/messages
        [HttpGet("teams/{teamId}/messages")]
        public async Task<IActionResult> GetTeamMessagesHistory(int teamId)
        {
            var messages = await chatService.GetGroupMessagesHistoryAsync(ChatGroup.Team, teamId);
            return messages == null ? NotFound("No messages found!") as IActionResult
                : Ok(messages);
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
