using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polyglot.DataAccess.Entities;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        // GET: Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject("value");
        }

        // POST: Users
        [HttpPost]
        public void Post([FromBody] UserProfile value)
        {
        }

        // PUT: Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserProfile value)
        {
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
