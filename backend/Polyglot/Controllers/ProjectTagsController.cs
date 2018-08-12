using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectTagsController : ControllerBase
    {
        private readonly ICRUDService service;

        public ProjectTagsController(ICRUDService service)
        {
            this.service = service;
        }

        // GET: ProjectTags
        [HttpGet]
        public async Task<IActionResult> GetAllProjectTags()
        {
            var projects = await service.GetListAsync<ProjectTag, ProjectTagDTO>();
            return projects == null ? NotFound("No project tags found!") as IActionResult
                : Ok(projects);
        }

        // GET: ProjectTags/5
        [HttpGet("{id}", Name = "GetProjectTag")]
        public async Task<IActionResult> GetProjectTag(int id)
        {
            var project = await service.GetOneAsync<ProjectTag, ProjectTagDTO>(id);
            return project == null ? NotFound($"ProjectTag with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: ProjectTags
        public async Task<IActionResult> AddProjectTag([FromBody]ProjectTagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync<ProjectTag, ProjectTagDTO>(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: ProjectTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectTag(int id, [FromBody]ProjectTagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync<ProjectTag, ProjectTagDTO>(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTag(int id)
        {
            var success = await service.TryDeleteAsync<ProjectTag>(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
