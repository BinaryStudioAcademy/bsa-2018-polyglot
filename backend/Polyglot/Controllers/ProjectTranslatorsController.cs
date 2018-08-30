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
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectTranslatorsController : Controller
    {
        private IProjectService service;
        private readonly ISignalrWorkspaceService signalrService;

        public ProjectTranslatorsController(IProjectService projectService, ISignalrWorkspaceService signalrService)
        {
            this.service = projectService;
            this.signalrService = signalrService;
        }
    }
}
