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
    [Route("[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Language, int> service;

        public LanguagesController(ICRUDService<Language, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Languages
        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No languages found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<LanguageDTO>>(projects));
        }

        // GET: Languages/5
        [HttpGet("{id}", Name = "GetLanguage")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Language with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<LanguageDTO>(project));
        }

        // POST: Languages
        public async Task<IActionResult> AddLanguage([FromBody]LanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Language>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<LanguageDTO>(entity));
        }

        // PUT: Languages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyLanguage(int id, [FromBody]LanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Language>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<LanguageDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
