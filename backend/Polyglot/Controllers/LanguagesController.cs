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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService service;
        private readonly ICurrentUser _currentUser;

        public LanguagesController(ILanguageService service, ICurrentUser currentUser)
        {
            this.service = service;
            _currentUser = currentUser;
        }

        // GET: Languages
        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No languages found!") as IActionResult
                : Ok(projects);
        }

        // GET: Languages/5
        [HttpGet("{id}", Name = "GetLanguage")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Language with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        [HttpPost]
        // POST: Languages
        public async Task<IActionResult> AddLanguage([FromBody]LanguageDTO language)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(language);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Languages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyLanguage(int id, [FromBody]LanguageDTO language)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(language);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        // PUT: Languages/5
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserLanguages(int id)
        {

            var entities = await service.GetTranslatorLanguages(id);
            return entities == null ? NotFound($"This user hasn't languages") as IActionResult
                : Ok(entities);
        }

        // PUT: Languages/user
        [HttpPut("user")]
        public async Task<IActionResult> SetCurrenUserLanguages([FromBody]TranslatorLanguageDTO[] languages)
        {
            var entity = await service.SetTranslatorLanguages((await _currentUser.GetCurrentUserProfile()).Id, languages);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        [HttpDelete("user")]
        public async Task<IActionResult> DeleteCurrenUserLanguages([FromBody]TranslatorLanguageDTO[] languages)
        {
            var entity = await service.DeleteTranslatorsLanguages((await _currentUser.GetCurrentUserProfile()).Id, languages);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }
    }
}
