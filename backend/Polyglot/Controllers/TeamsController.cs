﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Helpers;

namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService service;
        private readonly IRightService rightService;
        private readonly ICurrentUser _currentUser;
        private readonly ICRUDService<TeamTranslator, TranslatorDTO> teamTranslatorService;


        public TeamsController(ITeamService service, ICRUDService<TeamTranslator, TranslatorDTO> teamTranslatorService, IMapper mapper,
                                IRightService rightService, ICurrentUser currentUser)
        {
            this.service = service;
            this.teamTranslatorService = teamTranslatorService;
            this.rightService = rightService;
            _currentUser = currentUser;
        }

        // GET: Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await service.GetAllTeamsAsync();
            return teams == null ? NotFound("No teams found!") as IActionResult
                : Ok(teams);
        }

		[HttpGet("search")]
		public async Task<IActionResult> SearchTeams(string query)
		{
			if (query == null)
				query = "";

			var projects = await service.SearchTeams(query);
			return projects == null ? NotFound("No Teams found!") as IActionResult
				: Ok(projects);
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

		// GET: teams/translators
		[HttpGet("filteredtranslators", Name = "GetFilteredtranslators")]
		public async Task<IActionResult> GetFilteredtranslators([FromQuery(Name = "prof")] int prof, [FromQuery(Name = "languages")] int[] languages)
		{
			var translators = await service.GetFilteredtranslators(prof, languages);
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

        [HttpPut("translators")]


        public async Task<IActionResult> AddTeamTranslators([FromBody]TeamTranslatorsDTO teamTransalors)
        {
            var entity = await service.TryAddTeamAsync(teamTransalors);

            return Ok(entity);

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


        [HttpPut("{teamId}/activate")]
        public async Task<IActionResult> ActivateCurrentUser(int teamId)
        {
            int currentUserId = (await _currentUser.GetCurrentUserProfile()).Id;
            var entity = await service.ActivateUserInTeam(currentUserId, teamId);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        [HttpDelete("{teamId}/removeUser/{userId}")]
        public async Task<IActionResult> RemoveUserFromTeam(int teamId, int userId)
        {
            
            var entity = await service.DeleteUserFromTeam(userId, teamId);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

    }
}
