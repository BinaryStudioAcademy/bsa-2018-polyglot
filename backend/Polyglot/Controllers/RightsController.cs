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
    public class RightsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Right> service;

        public RightsController(ICRUDService<Right> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Rights
        [HttpGet]
        public async Task<IActionResult> GetAllRights()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No rights found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<RightDTO>>(projects));
        }

        // GET: Rights/5
        [HttpGet("{id}", Name = "GetRight")]
        public async Task<IActionResult> GetRight(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Right with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<RightDTO>(project));
        }

        // POST: Rights
        public async Task<IActionResult> AddRight([FromBody]RightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Right>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<RightDTO>(entity));
        }

        // PUT: Rights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRight(int id, [FromBody]RightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Right>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<RightDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRight(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
