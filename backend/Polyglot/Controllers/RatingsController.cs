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
    public class RatingsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<Rating, int> service;

        public RatingsController(ICRUDService<Rating, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Ratings
        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var projects = await service.GetListAsync();
            return projects == null ? NotFound("No ratings found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<RatingDTO>>(projects));
        }

        // GET: Ratings/5
        [HttpGet("{id}", Name = "GetRating")]
        public async Task<IActionResult> GetRating(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Rating with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<RatingDTO>(project));
        }

        // POST: Ratings
        public async Task<IActionResult> AddRating([FromBody]RatingDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<Rating>(project));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<RatingDTO>(entity));
        }

        // PUT: Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRating(int id, [FromBody]RatingDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<Rating>(project));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<RatingDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
