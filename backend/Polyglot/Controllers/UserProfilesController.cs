using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class UserProfilesController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IRatingService ratingService;
        private readonly ITeamService teamService;
        private readonly IFileStorageProvider fileStorageProvider;
        private readonly IMapper mapper;
        private readonly IRightService rightService;
        private readonly ICurrentUser _currentUser;


        public UserProfilesController(IUserService service, IRatingService ratingService, ITeamService teamService, IFileStorageProvider fileStorageProvider, IMapper mapper,
                                      IRightService rightService, ICurrentUser currentUser)
        {
            this.service = service;
            this.ratingService = ratingService;
            this.teamService = teamService;
            this.fileStorageProvider = fileStorageProvider;
            this.rightService = rightService;
            _currentUser = currentUser;
            this.mapper = mapper;
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
                : Ok(userRatings.Reverse());
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UserProfileDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;
                      
            var entity = await service.PutAsync(user);

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

            if (user.AvatarUrl == null || user.AvatarUrl == "")
            {
                user.AvatarUrl = "/assets/images/default-avatar.jpg";
            }

            user.RegistrationDate = DateTime.UtcNow;
            var entity = await service.PostAsync(user);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        [HttpPut("photo")]
        public async Task<IActionResult> AddCropedPhoto(IFormFile formFile)
        {
            var currentUser = mapper.Map<UserProfileDTO>(await _currentUser.GetCurrentUserProfile());
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (Request.Form.Files.Count != 0)
            {
                IFormFile photo = Request.Form.Files[0];
                byte[] byteArr;
                using (var ms = new MemoryStream())
                {
                    photo.CopyTo(ms);
                    await photo.CopyToAsync(ms);
                    byteArr = ms.ToArray();
                }

                currentUser.AvatarUrl = await fileStorageProvider.UploadFileAsync(byteArr, FileType.Photo, Path.GetExtension(photo.FileName));
                var result = await service.PutUserBool(currentUser);

                return currentUser == null
                    ? StatusCode(400) as IActionResult
                    : Ok(currentUser);
            }

            return BadRequest();
        }

        [HttpGet("isInDb")]
        public async Task<bool> IsUserInDb()
        {
            return await service.IsExistByUidAsync();
        }

        [HttpGet("rights")]
        public async Task<IActionResult> GetAllRights()
        {
            var rights = (await rightService.GetUserRights());
            return rights == null ? NotFound($"Rights not found!") as IActionResult
               : Ok(rights);
        }

        [HttpGet("rights/{projectId}")]
        public async Task<IActionResult> GetRightsByProject(int projectId)
        {
            var rights = (await rightService.GetUserRightsInProject(projectId));
            return rights == null ? NotFound($"Rights not found!") as IActionResult
               : Ok(rights);
        }

        // GET: UserProfiles
        [HttpGet("name/{fullName}")]
        public async Task<IActionResult> GetUserProfilesByNameStartWith(string fullName)
        {
            var user = await service.GetUsersByNameStartsWith(fullName);
            return user == null ? NotFound($"Users not found!") as IActionResult
               : Ok(user);
        }
    }
}
