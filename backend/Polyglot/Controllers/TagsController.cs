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
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Tag, int> service;

        public TagsController(ICRUDService<Tag, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Tags
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No tags found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<TagDTO>>(projects));
        }

        // GET: Tags/5
        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> GetTag(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Tag with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<TagDTO>(project));
        }

        // POST: Tags
        public async Task<IActionResult> AddTag([FromBody]TagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Tag>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<TagDTO>(entity));
        }

        // PUT: Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTag(int id, [FromBody]TagDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Tag>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<TagDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
