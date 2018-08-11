using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ICRUDService service;

        public TagsController(ICRUDService service)
        {
            this.service = service;
        }

        // GET: Tags
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var projects = await service.GetListAsync<Tag, TagDTO>();
            return projects == null ? NotFound("No tags found!") as IActionResult
                : Ok(projects);
        }

        // GET: Tags/5
        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> GetTag(int id)
        {
            var project = await service.GetOneAsync<Tag, TagDTO>(id);
            return project == null ? NotFound($"Tag with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Tags
        public async Task<IActionResult> AddTag([FromBody]TagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync<Tag, TagDTO>(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTag(int id, [FromBody]TagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync<Tag, TagDTO>(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var success = await service.TryDeleteAsync<Tag>(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
