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
    public class ProjectGlossariesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<ProjectGlossary, int> service;

        public ProjectGlossariesController(ICRUDService<ProjectGlossary, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: ProjectGlossary
        [HttpGet]
        public async Task<IActionResult> GetAllProjectGlossary()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project glossaries found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ProjectGlossaryDTO>>(projects));
        }

        // GET: ProjectGlossary/5
        [HttpGet("{id}", Name = "GetProjectGlossary")]
        public async Task<IActionResult> GetProjectGlossary(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectGlossary with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ProjectGlossaryDTO>(project));
        }

        // POST: ProjectGlossary
        public async Task<IActionResult> AddProjectGlossary([FromBody]ProjectGlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<ProjectGlossary>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ProjectGlossaryDTO>(entity));
        }

        // PUT: ProjectGlossary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectGlossary(int id, [FromBody]ProjectGlossaryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<ProjectGlossary>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ProjectGlossaryDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectGlossary(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
