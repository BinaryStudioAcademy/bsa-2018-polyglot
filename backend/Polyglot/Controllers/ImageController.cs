using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public ImageController(IHostingEnvironment hostingEnvironvent)
        {
            _hostingEnvironment = hostingEnvironvent;
        }
        // POST: Image
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {

            IFormFile file = Request.Form.Files[0];
            return Ok(file);
        }
    }
    }

