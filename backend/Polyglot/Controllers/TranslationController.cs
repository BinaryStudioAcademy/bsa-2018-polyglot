using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.TranslationServices;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslatorProvider _provider;

        public TranslationController(ITranslatorProvider provider)
        {
            _provider = provider;
        }

        [HttpPost]
        public async Task<IActionResult> Translate([FromBody] TextForTranslation item)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await _provider.Translate(item);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity}",
                    entity);
        }
    }
}
