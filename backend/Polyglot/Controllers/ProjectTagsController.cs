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
    public class ProjectTagsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<ProjectTag> service;

        public ProjectTagsController(ICRUDService<ProjectTag> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: ProjectTags
        [HttpGet]
        public async Task<IActionResult> GetAllProjectTags()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project tags found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ProjectTagDTO>>(projects));
        }

        // GET: ProjectTags/5
        [HttpGet("{id}", Name = "GetProjectTag")]
        public async Task<IActionResult> GetProjectTag(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectTag with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ProjectTagDTO>(project));
        }

        // POST: ProjectTags
        public async Task<IActionResult> AddProjectTag([FromBody]ProjectTagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<ProjectTag>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ProjectTagDTO>(entity));
        }

        // PUT: ProjectTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectTag(int id, [FromBody]ProjectTagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<ProjectTag>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ProjectTagDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTag(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
