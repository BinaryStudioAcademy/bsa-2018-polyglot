using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectTranslatorsController : ControllerBase
    {
        private IProjectTranslatorsService service;

        public ProjectTranslatorsController(IProjectTranslatorsService projectTranslatorsService)
        {
            this.service = projectTranslatorsService;
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
