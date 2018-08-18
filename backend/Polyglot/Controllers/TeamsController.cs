using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService service;

        public TeamsController(ITeamService service, IMapper mapper)
        {
            this.service = service;
        }

        // GET: Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await service.GetAllTeamsPrevsAsync();
            return teams == null ? NotFound("No teams found!") as IActionResult
                : Ok(teams);
        }

        // GET: Teams/5
        [HttpGet("{id}", Name = "GetTeammates")]
        public async Task<IActionResult> GetTeammates(int id)
        {
            var teamTranslators = await service.GetTeamTranslatorsAsync(id);
            return teamTranslators == null ? NotFound($"No teammates found for team with id = {id}!") as IActionResult
                : Ok(teamTranslators);
        }

        // GET: teams/translators
        [HttpGet("translators", Name = "GetAllTranslators")]
        public async Task<IActionResult> GetAllTranslators()
        {
            var translators = await service.GetAllTranslatorsAsync();
            return translators == null ? NotFound("No translators found!") as IActionResult
                : Ok(translators);
        }

        // GET: teams/translators/:id/rating
        [HttpGet("translators/{id}/rating", Name = "GetTranslatorRating")]
        public async Task<IActionResult> GetTranslatorRating(int translatorId)
        {
            var translatorRating = await service.GetTranslatorRatingValueAsync(translatorId);
            return Ok(translatorRating);
        }

        //// POST: Teams
        //public async Task<IActionResult> FormTeam([FromBody]TeamDTO team)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest() as IActionResult;

        //    var entity = await service.PostAsync(team);
        //    return entity == null ? StatusCode(409) as IActionResult
        //        : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
        //        entity);
        //}

        // POST: Teams
        public async Task<IActionResult> FormTeam([FromBody]int[] translatorsIds)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

#warning у команды пока что нет менеджера
            var entity = await service.FormTeamAsync(translatorsIds, -1);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
        }

        // PUT: Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyTeam(int id, [FromBody]TeamDTO team)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(team);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisbandTeam(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
