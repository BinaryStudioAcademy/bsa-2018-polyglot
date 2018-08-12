using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslatorRightsController : ControllerBase
    {
        private readonly ICRUDService service;

        public TranslatorRightsController(ICRUDService service)
        {
            this.service = service;
        }

        // GET: TranslatorRights
        [HttpGet]
        public async Task<IActionResult> GetAllTranslatorRights()
        {
            var projects = await service.GetListAsync<TranslatorRight, TranslatorRightDTO>();
            return projects == null ? NotFound("No translator rights found!") as IActionResult
                : Ok(projects);
        }

        // GET: TranslatorRights/5
        [HttpGet("{id}", Name = "GetTranslatorRight")]
        public async Task<IActionResult> GetTranslatorRight(int id)
        {
            var project = await service.GetOneAsync<TranslatorRight, TranslatorRightDTO>(id);
            return project == null ? NotFound($"TranslatorRight with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: TranslatorRights
        public async Task<IActionResult> AddTranslatorRight([FromBody]TranslatorRightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync<TranslatorRight, TranslatorRightDTO>(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: TranslatorRights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslatorRight(int id, [FromBody]TranslatorRightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync<TranslatorRight, TranslatorRightDTO>(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatorRight(int id)
        {
            var success = await service.TryDeleteAsync<TranslatorRight>(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
