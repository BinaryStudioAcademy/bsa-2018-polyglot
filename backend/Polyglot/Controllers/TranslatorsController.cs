using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class TranslatorsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Translator, int> service;

        public TranslatorsController(ICRUDService<Translator, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Translators
        [HttpGet]
        public async Task<IActionResult> GetAllTranslators()
        {
            var translators = await service.GetListAsync();
            return translators == null ? NotFound("No projects found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<TranslatorDTO>>(translators));
        }

        // GET: Translators/5
        [HttpGet("{id}", Name = "GetTranslator")]
        public async Task<IActionResult> GetTranslator(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Translator with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<TranslatorDTO>(project));
        }

        // POST: Translators
        public async Task<IActionResult> AddTranslator([FromBody]TranslatorDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Translator>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<TranslatorDTO>(entity));
        }

        // PUT: Translators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslator(int id, [FromBody]TranslatorDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Translator>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<TranslatorDTO>(entity));
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
