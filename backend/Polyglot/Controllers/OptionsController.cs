using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly ICRUDService<Option, OptionDTO> service;

        public OptionsController(ICRUDService<Option, OptionDTO> service)
        {
            this.service = service;
        }

        // GET: Options
        [HttpGet]
        public async Task<IActionResult> GetAllOptions()
        {
            var options = await service.GetListAsync();
            return options == null ? NotFound("No options found!") as IActionResult
                : Ok(options);
        }

        // GET: Options/5
        [HttpGet("{id}", Name = "GetOption")]
        public async Task<IActionResult> GetOption(int id)
        {
            var options = await service.GetOneAsync(id);
            return options == null ? NotFound($"Option with id = {id} not found!") as IActionResult
                : Ok(options);
        }

        // POST: Options
        public async Task<IActionResult> AddOption([FromBody]OptionDTO option)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(option);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Options/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyOption(int id, [FromBody]OptionDTO option)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(option);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: Options/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}