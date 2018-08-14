using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class TranslatorsController : ControllerBase
    {
        private readonly ICRUDService<Translator, TranslatorDTO> service;

        public TranslatorsController(ICRUDService<Translator, TranslatorDTO> service)
        {
            this.service = service;
        }

        // GET: Translators
        [HttpGet]
        public async Task<IActionResult> GetAllTranslators()
        {
            var translators = await service.GetListAsync();
            return translators == null ? NotFound("No projects found!") as IActionResult
                : Ok(translators);
        }

        // GET: Translators/5
        [HttpGet("{id}", Name = "GetTranslator")]
        public async Task<IActionResult> GetTranslator(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Translator with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Translators
        public async Task<IActionResult> AddTranslator([FromBody]TranslatorDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Translators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslator(int id, [FromBody]TranslatorDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslator(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
