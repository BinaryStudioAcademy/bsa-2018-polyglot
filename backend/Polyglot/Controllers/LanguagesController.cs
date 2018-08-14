using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ICRUDService<Language, LanguageDTO> service;

        public LanguagesController(ICRUDService<Language, LanguageDTO> service)
        {
            this.service = service;
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
        public async Task<IActionResult> AddLanguage([FromBody]LanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Languages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyLanguage(int id, [FromBody]LanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
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
    }
}
