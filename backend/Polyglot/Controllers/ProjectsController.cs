using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
		private IProjectService service;
        

        public ProjectsController(IProjectService projectService)
        {
			this.service =  projectService;
        }


        // GET: Projects

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {

            var projects = await service.GetListAsync<Project, ProjectDTO>();

            return projects == null ? NotFound("No projects found!") as IActionResult
                : Ok(projects);
        }

        // GET: Projects/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> GetProject(int id)
        {

            var project = await service.GetOneAsync<Project, ProjectDTO>(id);

            return project == null ? NotFound($"Project with id = {id} not found!") as IActionResult
                : Ok(project);

        }

        // Get: Projects/5/complexString
        [HttpGet("{id}/complexStrings", Name = "GetProjectStrings")]
        public async Task<IActionResult> GetProjectStrings(int id)
        {
            var projectsStrings = await service.GetProjectStringsAsync(id);
            return projectsStrings == null ? NotFound("No project strings found!") as IActionResult
                : Ok(projectsStrings);
        }

        // POST: Projects
        public async Task<IActionResult> AddProject([FromBody]ProjectDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync<Project, ProjectDTO>(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);

        }

        // PUT: Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProject(int id, [FromBody]ProjectDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync<Project, ProjectDTO>(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var success = await service.TryDeleteAsync<Project>(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
		
		[HttpPost]
		[Route("dictionary")]
		public async Task<IActionResult> AddFileDictionary(IFormFile files)
		{
			await service.FileParseDictionary(Request.Form.Files[0]);
			return Ok();
		}
	}
}
