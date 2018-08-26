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
        private readonly IGlossaryService service;

        public GlossariesController(IGlossaryService service)
        {
            this.service = service;
        }

        // GET: Glossaries
        [HttpGet]
        public async Task<IActionResult> GetAllGlossaries()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No glossaries found!") as IActionResult
                : Ok(projects);
        }

        // GET: Glossaries/5
        [HttpGet("{id}", Name = "GetGlossary")]
        public async Task<IActionResult> GetGlossary(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Glossary with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Glossaries
        public async Task<IActionResult> AddGlossary([FromBody]GlossaryDTO glossary)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(glossary);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Glossaries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyGlossary(int id, [FromBody]GlossaryDTO glossary)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(glossary);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlossary(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        // POST: Glossaries/5/strings
        [HttpPost("{id}/strings")]
        public async Task<IActionResult> AddString(int id, [FromBody]GlossaryStringDTO glossaryString)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.AddString(id, glossaryString);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // PUT: Glossaries/5/strings
        [HttpPut("{id}/strings")]
        public async Task<IActionResult> EditString(int id, [FromBody]GlossaryStringDTO glossaryString)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.UpdateString(id, glossaryString);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: Glossaries/5/strings
        [HttpDelete("{id}/strings")]
        public async Task<IActionResult> DeleteString(int id, [FromBody]GlossaryStringDTO glossaryString)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.DeleteString(id, glossaryString);
            return entity == false ? StatusCode(304) as IActionResult
                : Ok(entity);
        }
    }
}
