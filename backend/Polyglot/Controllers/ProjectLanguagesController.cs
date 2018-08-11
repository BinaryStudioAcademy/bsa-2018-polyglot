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
    public class ProjectLanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<ProjectLanguage> service;

        public ProjectLanguagesController(ICRUDService<ProjectLanguage> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: ProjectLanguages
        [HttpGet]
        public async Task<IActionResult> GetAllProjectLanguages()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project languages found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ProjectLanguageDTO>>(projects));
        }

        // GET: ProjectLanguages/5
        [HttpGet("{id}", Name = "GetProjectLanguage")]
        public async Task<IActionResult> GetProjectLanguage(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectLanguage with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ProjectLanguageDTO>(project));
        }

        // POST: ProjectLanguages
        public async Task<IActionResult> AddProjectLanguage([FromBody]ProjectLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<ProjectLanguage>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ProjectLanguageDTO>(entity));
        }

        // PUT: ProjectLanguages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectLanguage(int id, [FromBody]ProjectLanguageDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<ProjectLanguage>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ProjectLanguageDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectLanguage(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
