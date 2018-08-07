using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using AutoMapper;

namespace Polyglot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Team, int> service;

        public TeamsController(ICRUDService<Team, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No teams found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<TeamDTO>>(projects));
        }

        // GET: api/Teams/5
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Team with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<TeamDTO>(project));
        }

        // POST: api/Teams
        public async Task<IActionResult> AddTeam([FromBody]TeamDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Team>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<TeamDTO>(entity));
        }

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTeam(int id, [FromBody]TeamDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Team>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<TeamDTO>(entity));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
