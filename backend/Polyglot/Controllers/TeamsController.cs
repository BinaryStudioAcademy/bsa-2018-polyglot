using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using AutoMapper;
using System.Collections.Generic;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Helpers;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService service;
        private readonly IRightService rightService;
        private readonly ICRUDService<TeamTranslator, TranslatorDTO> teamTranslatorService;

        public TeamsController(ITeamService service, ICRUDService<TeamTranslator, TranslatorDTO> teamTranslatorService, IMapper mapper,
                                IRightService rightService)
        {
            this.service = service;
            this.teamTranslatorService = teamTranslatorService;
            this.rightService = rightService;
        }

        // GET: Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
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
        public async Task<IActionResult> FormTeam([FromBody]ReceiveTeamDTO translatorIds)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await service.FormTeamAsync(translatorIds);
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

        [HttpDelete("translators")]
        public async Task<IActionResult> RemoveTeamTranslators([FromBody]int[] teamTranslatorIds)
        {
            foreach (int id in teamTranslatorIds)
            {
                var entity = await teamTranslatorService.TryDeleteAsync(id);
                if (!entity)
                    return BadRequest();
            }

            return Ok();
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DisbandTeam(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        [HttpPut("{teamId}/addTranslatorRight/{userId}")]
        public async Task<IActionResult> AddRightToTranslator(int teamId, int userId, [FromBody]RightDefinition rightDefinition)
        {
            var entity = await rightService.SetTranslatorRight(userId, teamId, rightDefinition);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        [HttpPut("{teamId}/removeTranslatorRight/{userId}")]
        public async Task<IActionResult> RemoveRightFromTranslator(int teamId, int userId, [FromBody]RightDefinition rightDefinition)
        {
            var entity = await rightService.RemoveTranslatorRight(userId, teamId, rightDefinition);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        [HttpGet("{teamId}/user/{userId}/right/{rightDefinition}")]
        public async Task<bool> CheckIfUserCan(int teamId, int userId, RightDefinition rightDefinition)
        {
            return await rightService.CheckIfUserCan(userId, teamId, rightDefinition);
        }
    }
}
