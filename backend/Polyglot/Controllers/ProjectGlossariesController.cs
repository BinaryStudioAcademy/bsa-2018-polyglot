using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectGlossariesController : ControllerBase
    {
        private readonly ICRUDService<ProjectGlossary, ProjectGlossaryDTO> service;

        public ProjectGlossariesController(ICRUDService<ProjectGlossary, ProjectGlossaryDTO> service)
        {
            this.service = service;
        }

        // GET: ProjectGlossary
        [HttpGet]
        public async Task<IActionResult> GetAllProjectGlossary()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project glossaries found!") as IActionResult
                : Ok(projects);
        }

        // GET: ProjectGlossary/5
        [HttpGet("{id}", Name = "GetProjectGlossary")]
        public async Task<IActionResult> GetProjectGlossary(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectGlossary with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: ProjectGlossary
        public async Task<IActionResult> AddProjectGlossary([FromBody]ProjectGlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: ProjectGlossary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectGlossary(int id, [FromBody]ProjectGlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectGlossary(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
