﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesStorageController : ControllerBase
    {
        public IFileStorageProvider fileStorageProvider;
        public FilesStorageController(IFileStorageProvider provider)
        {
            fileStorageProvider = provider;
        }

        // GET: Files
        [HttpPost]
        public async Task<IActionResult> UploadFileInStorage()
        {
            IFormFile file = Request.Form.Files[0];
            byte[] byteArr;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                await file.CopyToAsync(ms);
                byteArr = ms.ToArray();
            }

            return Ok( await fileStorageProvider.UploadFileAsync(byteArr, FileType.Photo, Path.GetExtension(file.FileName)));
        }


    }
}