using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplexStringsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMongoRepository<ComplexString> dataProvider;
        //TODO: change IRepository<ComplexString> to IComplexStringService
        public ComplexStringsController(IMongoRepository<ComplexString> dataProvider, IMapper mapper)
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
        }

        // GET: ComplexStrings
        [HttpGet]
        public async Task<IActionResult> GetAllComplexStrings()
        {
            var complexStrings = await dataProvider.GetAllAsync();
            return complexStrings == null ? NotFound("No files found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ComplexStringDTO>>(complexStrings));
        }

        // GET: ComplexStrings/5
        [HttpGet("{id}", Name = "GetcomplexStringComplexString")]
        public async Task<IActionResult> GetcomplexStringComplexString(int id)
        {
            var complexString = await dataProvider.GetAsync(id);
            return complexString == null ? NotFound($"ComplexString with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ComplexStringDTO>(complexString));
        }

        // POST: ComplexStrings
        public async Task<IActionResult> AddComplexString([FromBody]ComplexStringDTO complexString)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await dataProvider.CreateAsync(mapper.Map<ComplexString>(complexString));
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

            var cs = mapper.Map<ComplexString>(complexString);
            cs.Id = id;

            var entity = dataProvider.Update(cs);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ComplexStringDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplexString(int id)
        {
            var success = await dataProvider.DeleteAsync(id);
            return success == null ? Ok() : StatusCode(304);
        }
    }
}
