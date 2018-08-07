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
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectHistoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<ProjectHistory, int> service;

        public ProjectHistoriesController(ICRUDService<ProjectHistory, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/ProjectHistorys
        [HttpGet]
        public async Task<IActionResult> GetAllProjectHistorys()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No project histories found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ProjectHistoryDTO>>(projects));
        }

        // GET: api/ProjectHistorys/5
        [HttpGet("{id}", Name = "GetProjectHistory")]
        public async Task<IActionResult> GetProjectHistory(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"ProjectHistory with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ProjectHistoryDTO>(project));
        }

        // POST: api/ProjectHistorys
        public async Task<IActionResult> AddProjectHistory([FromBody]ProjectHistoryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<ProjectHistory>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ProjectHistoryDTO>(entity));
        }

        // PUT: api/ProjectHistorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectHistory(int id, [FromBody]ProjectHistoryDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<ProjectHistory>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ProjectHistoryDTO>(entity));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectHistory(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
