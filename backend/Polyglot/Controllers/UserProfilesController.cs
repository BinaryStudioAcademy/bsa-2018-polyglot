﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.Authentication.Extensions;
namespace Polyglot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]

    public class UserProfilesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICRUDService<UserProfile, int> service;

        public UserProfilesController(ICRUDService<UserProfile, int> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entities = await service.GetListAsync();
            return entities == null ? NotFound("Users not found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<UserProfileDTO>>(entities));
        }


        // GET: Users
        [HttpGet("user")]
        public string GetUser()
        {
            return UserNataliHelper.FullName;
        }

        // GET: Users/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await service.GetOneAsync(id);
            return entity == null ? NotFound($"User with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<UserProfileDTO>(entity));
        }

        // POST: Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserProfile value)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PostAsync(mapper.Map<UserProfile>(value));
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<UserProfileDTO>(entity));
        }

        // PUT: Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserProfile value)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.PutAsync(id, mapper.Map<UserProfile>(value));
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<UserProfileDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}
