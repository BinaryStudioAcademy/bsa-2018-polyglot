using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslatorLanguagesController : ControllerBase
    {
        private readonly ICRUDService<TranslatorLanguage, TranslatorLanguageDTO> service;

        public TranslatorLanguagesController(ICRUDService<TranslatorLanguage, TranslatorLanguageDTO> service, IMapper mapper)
        {
            this.service = service;
        }

        // GET: TranslatorLanguages
        [HttpGet]
        public async Task<IActionResult> GetAllTranslatorLanguages()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No translator languages found!") as IActionResult
                : Ok(projects);
        }

        // GET: TranslatorLanguages/5
        [HttpGet("{id}", Name = "GetTranslatorLanguage")]
        public async Task<IActionResult> GetTranslatorLanguage(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"TranslatorLanguage with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: TranslatorLanguages
        public async Task<IActionResult> AddTranslatorLanguage([FromBody]TranslatorLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: TranslatorLanguages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslatorLanguage(int id, [FromBody]TranslatorLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatorLanguage(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
