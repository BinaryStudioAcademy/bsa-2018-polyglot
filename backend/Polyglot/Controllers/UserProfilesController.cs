using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class UserProfilesController : ControllerBase
    {
        private readonly IUserService service;
        private readonly ICRUDService<Rating, RatingDTO> ratingService;
        private readonly ITeamService teamService;

        public UserProfilesController(IUserService service, ICRUDService<Rating, RatingDTO> ratingService, ITeamService teamService)
        {
            this.service = service;
            this.ratingService = ratingService;
            this.teamService = teamService;
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
        public async Task<IActionResult> GetUserByUid()
        {
            var user = await service.GetByUidAsync();
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

        [HttpGet("{id}/ratings")]
        public async Task<IActionResult> GetUserRatings(int id)
        {
            var ratings = await ratingService.GetListAsync();
            var userRatings = ratings?.Where(x => x.UserId == id);

            return userRatings == null
                ? NotFound($"Ratings for user with id = {id} not found") as IActionResult
                : Ok(userRatings);
        }

        [HttpGet("{id}/teams")]
        public async Task<IActionResult> GetUserTeams(int id)
        {
            var teams = await teamService.GetListAsync();
            var userTeams = teams?.Where(x => x.TeamTranslators.Any(y => y.UserId == id));

            return userTeams == null
                ? NotFound($"Teams for user with id = {id} not found") as IActionResult
                : Ok(userTeams);
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
            user.RegistrationDate = DateTime.UtcNow;
            var entity = await service.PostAsync(user);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        [HttpGet("isInDb")]
        public async Task<bool> IsUserInDb()
        {
            return await service.IsExistByUidAsync(HttpContext.User.GetUid());
        }


    }
}
