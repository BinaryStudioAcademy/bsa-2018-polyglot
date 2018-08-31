using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;

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

        public ProjectTranslatorsController(IProjectService projectService)
        {
            this.service = projectService;
        }
    }
}
