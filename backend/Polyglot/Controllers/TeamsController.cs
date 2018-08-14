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
        private readonly ICRUDService<Team, TeamDTO> service;

        public TeamsController(ICRUDService<Team, TeamDTO> service, IMapper mapper)
        {
            this.service = service;
        }

        // GET: Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var a = new List<TeamPrevDTO>()
            {
                new TeamPrevDTO()
                {
                    Id = 1,
                    Persons = new System.Collections.Generic.List<UserProfilePrevDTO>()
                    {
                        new UserProfilePrevDTO()
                        {
                            Id = 1,
                            AvatarUrl = "http://pics.wikireality.ru/upload/thumb/f/f4/82f2426f2971.jpg/300px-82f2426f2971.jpg"
                        },
                        new UserProfilePrevDTO()
                        {
                            Id = 2,
                            AvatarUrl = "https://img.pravda.com/images/doc/2/0/20883bd-putin2.jpg"
                        },
                        new UserProfilePrevDTO()
                        {
                            Id = 3,
                            AvatarUrl = "https://i.ytimg.com/vi/Q5Qy_3PeaCY/maxresdefault.jpg"
                        }
                    }
                }
            };
            return Ok(a);
            //var teams = await service.GetListAsync();
            //return teams == null ? NotFound("No teams found!") as IActionResult
            //    : Ok(teams);
        }

        // GET: Teams/5
        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await service.GetOneAsync(id);
            return team == null ? NotFound($"Team with id = {id} not found!") as IActionResult
                : Ok(team);
        }

        // POST: Teams
        public async Task<IActionResult> AddTeam([FromBody]TeamDTO team)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(team);
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
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
