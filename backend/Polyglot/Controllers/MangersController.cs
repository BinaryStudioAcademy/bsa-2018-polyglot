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
    public class ManagersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Manager> service;

        public ManagersController(ICRUDService<Manager> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Managers
        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No managers found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ManagerDTO>>(projects));
        }

        // GET: Managers/5
        [HttpGet("{id}", Name = "GetManager")]
        public async Task<IActionResult> GetManager(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Manager with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ManagerDTO>(project));
        }

        // POST: Managers
        public async Task<IActionResult> AddManager([FromBody]ManagerDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Manager>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ManagerDTO>(entity));
        }

        // PUT: Managers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyManager(int id, [FromBody]ManagerDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Manager>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ManagerDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
