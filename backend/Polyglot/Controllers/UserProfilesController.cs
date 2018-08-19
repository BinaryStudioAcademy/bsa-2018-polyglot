using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.Authentication;
using Polyglot.Authentication.Extensions;
using System.Linq;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class UserProfilesController : ControllerBase
    {
        private readonly ICRUDService<UserProfile, UserProfileDTO> service;
        
        public UserProfilesController(ICRUDService<UserProfile, UserProfileDTO> service)
        {
            this.service = service;
        }

        // GET: UserProfiles
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var entities = await service.GetListAsync();
            return entities == null ? NotFound("No user profiles found!") as IActionResult
                : Ok(entities);

        }

        // GET: UserProfiles
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            UserProfileDTO user = UserIdentityService.GetCurrentUser();
            return user == null ? NotFound($"User not found!") as IActionResult
               : Ok(user);
        }

        // GET: UserProfiles/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await service.GetOneAsync(id);
            return entity == null ? NotFound($"Translator with id = {id} not found!") as IActionResult
                : Ok(entity);
        }

        // PUT: UserProfiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslatorRight(int id, [FromBody]UserProfileDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: UserProfiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatorRight(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProfileDTO user)
        {
            var uid = HttpContext.User.GetUid();
            user.Uid = uid;
            if (user.FullName == null || user.FullName == "")
            {
                user.FullName = HttpContext.User.GetName();
                user.AvatarUrl = HttpContext.User.GetProfilePicture();
            }
            var entity = await service.PostAsync(user);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        [HttpGet("uid")]
        public async Task<IActionResult> GetUserByUID(string uid)
        {
            var entity = (await service.GetListAsync()).FirstOrDefault(u => u.Uid == uid);
            return entity == null ? NotFound($"Translator with id = {uid} not found!") as IActionResult
                : Ok(entity);
        }
    }
}
