using System.Collections.Generic;
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
        private readonly ITagService service;

        public TagsController(ITagService service)
        {
            this.service = service;
        }

        // GET: Tags
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No tags found!") as IActionResult
                : Ok(projects);
        }

        // GET: Tags/5
        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> GetTag(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Tag with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // PUT: Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTag(int id, [FromBody]TagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddTagsToProject(int id, [FromBody]List<TagDTO> tags)
        {
            var entity = await service.AddTagsToProject(tags,id);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }
    }
}
