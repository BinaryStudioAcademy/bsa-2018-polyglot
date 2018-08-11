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
    public class TranslatorRightsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<TranslatorRight> service;

        public TranslatorRightsController(ICRUDService<TranslatorRight> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: TranslatorRights
        [HttpGet]
        public async Task<IActionResult> GetAllTranslatorRights()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No translator rights found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<TranslatorRightDTO>>(projects));
        }

        // GET: TranslatorRights/5
        [HttpGet("{id}", Name = "GetTranslatorRight")]
        public async Task<IActionResult> GetTranslatorRight(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"TranslatorRight with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<TranslatorRightDTO>(project));
        }

        // POST: TranslatorRights
        public async Task<IActionResult> AddTranslatorRight([FromBody]TranslatorRightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<TranslatorRight>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<TranslatorRightDTO>(entity));
        }

        // PUT: TranslatorRights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTranslatorRight(int id, [FromBody]TranslatorRightDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<TranslatorRight>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<TranslatorRightDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslatorRight(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
