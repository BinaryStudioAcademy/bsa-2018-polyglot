using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService service;

        public RatingsController(IRatingService service)
        {
            this.service = service;
        }

        // GET: Ratings
        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var projects = (await service.GetListAsync());
            return projects == null ? NotFound("No ratings found!") as IActionResult
                : Ok(projects);
        }

        // GET: Ratings/5
        [HttpGet("{id}", Name = "GetRating")]
        public async Task<IActionResult> GetRating(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Rating with id = {id} not found!") as IActionResult
                : Ok(project);
        }

        // POST: Ratings
        public async Task<IActionResult> AddRating([FromBody]RatingDTO project)
        {
            project.CreatedAt = DateTime.Now;
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(project);
            return entity == null ? StatusCode(409) as IActionResult
                : Ok(entity);
        }

        // PUT: Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyRating(int id, [FromBody]RatingDTO project)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
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
