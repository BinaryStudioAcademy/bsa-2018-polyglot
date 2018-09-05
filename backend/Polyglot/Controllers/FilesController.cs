using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ICRUDService<File,FileDTO> service;

        public FilesController(ICRUDService<File, FileDTO> service)
        {
            this.service = service;
        }

        // GET: Files
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No files found!") as IActionResult
                : Ok(projects);
        }

        // GET: Files/5
        [HttpGet("{id}", Name = "GetFile")]
        public async Task<IActionResult> GetFile(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"File with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Files
        public async Task<IActionResult> AddFile([FromBody]FileDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Files/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyFile(int id, [FromBody]FileDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync( project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
