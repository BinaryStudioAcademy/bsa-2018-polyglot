using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using AutoMapper;
using System.Collections.Generic;
using Polyglot.DataAccess.Helpers;

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
#warning ??? наверное GetAllTeamsAsync должен возвращать только команды определенного менеджера
            var teams = await service.GetAllTeamsAsync();
            return teams == null ? NotFound("No teams found!") as IActionResult
                : Ok(teams);
        }

        // GET: teams/:id
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<IActionResult> GetTeam(int id)


        {
            var teamTranslators = await service.GetOneAsync(id);
            return teamTranslators == null ? NotFound($"No team found with id = {id}!") as IActionResult
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

        // GET: teams/translators/:id
        [HttpGet("translators/{id}", Name = "GetTranslator")]
        public async Task<IActionResult> GetTranslator(int id)
        {
            var translators = await service.GetTranslatorAysnc(id);
            return translators == null ? NotFound("No translators found!") as IActionResult
                : Ok(translators);
        }

        // GET: teams/translators/:id/rating
        [HttpGet("translators/{translatorId}/rating", Name = "GetTranslatorRating")]
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
        [HttpPost]
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

        [HttpPut("{teamId}/addTranslatorRight/{userId}")]
        public async Task<IActionResult> AddRightToTranslator(int teamId, int userId, [FromBody]int definition)
        {
            RightDefinition rightDefinition = (RightDefinition)definition;  //get right definition from number

            var entity = await service.SetTranslatorRight(userId, teamId, rightDefinition);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        [HttpPut("{teamId}/removeTranslatorRight/{userId}")]
        public async Task<IActionResult> RemoveRightFromTranslator(int teamId, int userId, [FromBody]int definition)
        {
            RightDefinition rightDefinition = (RightDefinition)definition;  //get right definition from number

            var entity = await service.RemoveTranslatorRight(userId, teamId, rightDefinition);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }
    }
}
