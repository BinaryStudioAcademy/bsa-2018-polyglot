using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectLanguagesController : ControllerBase
    {
        private readonly ICRUDService<ProjectLanguage, ProjectLanguageDTO> service;

        public ProjectLanguagesController(ICRUDService<ProjectLanguage, ProjectLanguageDTO> service)
        {
            this.service = service;
        }

        // GET: ProjectLanguages
        [HttpGet]
        public async Task<IActionResult> GetAllProjectLanguages()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project languages found!") as IActionResult
                : Ok(projects);
        }

        // GET: ProjectLanguages/5
        [HttpGet("{id}", Name = "GetProjectLanguage")]
        public async Task<IActionResult> GetProjectLanguage(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectLanguage with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: ProjectLanguages
        public async Task<IActionResult> AddProjectLanguage([FromBody]ProjectLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: ProjectLanguages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectLanguage(int id, [FromBody]ProjectLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectLanguage(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
