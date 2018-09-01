using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectTranslatorsController : ControllerBase
    {
        private IProjectTranslatorsService service;
        private readonly ISignalrWorkspaceService signalrService;

        public ProjectTranslatorsController(IProjectTranslatorsService projectTranslatorsService, ISignalrWorkspaceService signalrService)
        {
            this.service = projectTranslatorsService;
            this.signalrService = signalrService;
        }

        // GET: ProjectTranslators/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTranslators(int id)
        {
            var translators = await service.GetProjectTranslators(id);
            return translators == null ? NotFound($"Translators not found!") as IActionResult
                : Ok(translators);
        }
    }
}
