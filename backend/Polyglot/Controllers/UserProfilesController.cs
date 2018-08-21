using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.Authentication;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class UserProfilesController : ControllerBase
    {
        private readonly ICRUDService<UserProfile, UserProfileDTO> service;
        private readonly ICRUDService<Rating, RatingDTO> ratingService;
        
        public UserProfilesController(ICRUDService<UserProfile, UserProfileDTO> service, ICRUDService<Rating, RatingDTO> ratingService)
        {
            this.service = service;
            this.ratingService = ratingService;
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

        [HttpGet("{id}/ratings")]
        public async Task<IActionResult> GetUserRatings(int id)
        {
            var ratings = await ratingService.GetListAsync();
            var userRatings = ratings?.Where(x => x.UserId == id);

            return userRatings == null
                ? NotFound($"Ratings for user with id = {id} not found") as IActionResult
                : Ok(userRatings);
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
    }
}
