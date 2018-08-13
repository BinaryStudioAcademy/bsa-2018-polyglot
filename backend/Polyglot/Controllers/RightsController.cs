using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        private readonly ICRUDService<Right, RightDTO> service;

        public RightsController(ICRUDService<Right, RightDTO> service)
        {
            this.service = service;
        }

        // GET: Rights
        [HttpGet]
        public async Task<IActionResult> GetAllRights()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No rights found!") as IActionResult
                : Ok(projects);
        }

        // GET: Rights/5
        [HttpGet("{id}", Name = "GetRight")]
        public async Task<IActionResult> GetRight(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Right with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Rights
        public async Task<IActionResult> AddRight([FromBody]RightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Rights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRight(int id, [FromBody]RightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRight(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
