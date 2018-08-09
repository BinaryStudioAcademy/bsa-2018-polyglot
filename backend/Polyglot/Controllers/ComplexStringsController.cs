using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using System.Threading.Tasks;

namespace Polyglot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexStringsController : ControllerBase
    {
        private readonly ICRUDService<ComplexString, int> _service;


        public ComplexStringsController(ICRUDService<ComplexString, int> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var complexStrings = await _service.GetListAsync();
            return complexStrings == null ? NotFound("No complex strings found!") as IActionResult
                : Ok(complexStrings);
        }

        // GET api/complexStrings/5 - retrieves a specific complexString using either Id or InternalId (BSonId)
        [HttpGet("{id}")]
        public async Task<ComplexString> Get(int id)
        {
            return await _service.GetOneAsync(id) ?? new ComplexString();
        }

        // POST api/complexStrings - creates a new complexString
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ComplexString newComplexString)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await _service.PostAsync(newComplexString);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
               entity);
        }


        // POST api/complexStrings - creates a new complexString
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ComplexString updateComplexString)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await _service.PutAsync(updateComplexString.Id,updateComplexString);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
               entity);
        }


        // DELETE api/complexStrings/5 - deletes a specific complexString
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
    }
}