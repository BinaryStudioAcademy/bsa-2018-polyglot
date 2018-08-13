using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectHistoriesController : ControllerBase
    {
        private readonly ICRUDService<ProjectHistory, ProjectHistoryDTO> service;

        public ProjectHistoriesController(ICRUDService<ProjectHistory, ProjectHistoryDTO> service)
        {
            this.service = service;
        }

        // GET: ProjectHistorys
        [HttpGet]
        public async Task<IActionResult> GetAllProjectHistorys()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project histories found!") as IActionResult
                : Ok(projects);
        }

        // GET: ProjectHistorys/5
        [HttpGet("{id}", Name = "GetProjectHistory")]
        public async Task<IActionResult> GetProjectHistory(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectHistory with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: ProjectHistorys
        public async Task<IActionResult> AddProjectHistory([FromBody]ProjectHistoryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: ProjectHistorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectHistory(int id, [FromBody]ProjectHistoryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectHistory(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
