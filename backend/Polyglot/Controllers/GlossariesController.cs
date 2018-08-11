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
    public class GlossariesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Glossary> service;

        public GlossariesController(ICRUDService<Glossary> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Glossarys
        [HttpGet]
        public async Task<IActionResult> GetAllGlossarys()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No glossaries found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<GlossaryDTO>>(projects));
        }

        // GET: Glossarys/5
        [HttpGet("{id}", Name = "GetGlossary")]
        public async Task<IActionResult> GetGlossary(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Glossary with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<GlossaryDTO>(project));
        }

        // POST: Glossarys
        public async Task<IActionResult> AddGlossary([FromBody]GlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Glossary>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<GlossaryDTO>(entity));
        }

        // PUT: Glossarys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyGlossary(int id, [FromBody]GlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Glossary>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<GlossaryDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGlossary(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
