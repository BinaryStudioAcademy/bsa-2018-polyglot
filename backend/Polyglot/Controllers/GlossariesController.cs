using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GlossariesController : ControllerBase
    {
        private readonly ICRUDService service;

        public GlossariesController(ICRUDService service)
        {
            this.service = service;
        }

        // GET: Glossarys
        [HttpGet]
        public async Task<IActionResult> GetAllGlossarys()
        {
            var projects = await service.GetListAsync<Glossary, GlossaryDTO>();
            return projects == null ? NotFound("No glossaries found!") as IActionResult
                : Ok(projects);
        }

        // GET: Glossarys/5
        [HttpGet("{id}", Name = "GetGlossary")]
        public async Task<IActionResult> GetGlossary(int id)
        {
            var project = await service.GetOneAsync<Glossary, GlossaryDTO>(id);
            return project == null ? NotFound($"Glossary with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Glossarys
        public async Task<IActionResult> AddGlossary([FromBody]GlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync<Glossary, GlossaryDTO>(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Glossarys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyGlossary(int id, [FromBody]GlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync<Glossary, GlossaryDTO>(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlossary(int id)
        {
            var success = await service.TryDeleteAsync<Glossary>(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
