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
    public class FilesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<File, int> service;

        public FilesController(ICRUDService<File, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No files found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<FileDTO>>(projects));
        }

        // GET: api/Files/5
        [HttpGet("{id}", Name = "GetFile")]
        public async Task<IActionResult> GetFile(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"File with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<FileDTO>(project));
        }

        // POST: api/Files
        public async Task<IActionResult> AddFile([FromBody]FileDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<File>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<FileDTO>(entity));
        }

        // PUT: api/Files/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyFile(int id, [FromBody]FileDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<File>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<FileDTO>(entity));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
