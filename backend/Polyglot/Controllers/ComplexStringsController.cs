using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplexStringsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IComplexStringService dataProvider;
        private readonly IProjectService service;

        public ComplexStringsController(IComplexStringService dataProvider, IProjectService service, IMapper mapper)
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
            this.service = service;
        }

        // POST: ComplexStrings
        [HttpPost]
        public async Task<IActionResult> AddComplexString([FromBody]ComplexStringDTO complexString)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await dataProvider.AddComplexString(complexString);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                mapper.Map<ComplexStringDTO>(entity));
        }

        // PUT: ComplexStrings/5
        [HttpPut("{id}")]
        public IActionResult ModifyComplexString(int id, [FromBody]ComplexStringDTO complexString)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            complexString.Id = id;

            var entity = dataProvider.ModifyComplexString(complexString);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ComplexStringDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplexString(int id)
        {
            var success = await dataProvider.DeleteComplexString(id);
            return success == null ? Ok() : StatusCode(304);
        }
    }
}
