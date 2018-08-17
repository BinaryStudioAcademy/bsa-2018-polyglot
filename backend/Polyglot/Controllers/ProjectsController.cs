using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Polyglot.Authentication;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
		private IProjectService service;

        public IFileStorageProvider fileStorageProvider;
        public ProjectsController(IProjectService projectService, IFileStorageProvider provider)
        {
			this.service =  projectService;
            fileStorageProvider = provider;
        }


        // GET: Projects

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var user = UserIdentityService.GetCurrentUser();
            if (user.Id == 0)
                return Ok();
            var projects = await service.GetListAsync(user.Id);
            return projects == null ? NotFound("No projects found!") as IActionResult
                : Ok(projects);
        }

        // GET: Projects/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await service.GetOneAsync(id);
            return project == null ? NotFound($"Project with id = {id} not found!") as IActionResult
                : Ok(project);

        }

        // GET: Projects/5/languages
        [HttpGet("{id}/languages", Name = "GetProjectLanguages")]
        public async Task<IActionResult> GetProjectLangs(int id)
        {
            var project = await service.GetProjectLanguages(id);
            return project == null ? NotFound($"Project with id = {id} has got no languages!") as IActionResult
                : Ok(project);

        }

        // PUT: Projects/:id/languages/:id
        [HttpPut("{projectId}/languages/{languageId}")]
        public async Task<IActionResult> AddLanguageToProject(int projectId, int languageId)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.AddLanguageToProject(projectId, languageId);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        //DELETE: projects/:id/languages/:id
        [HttpDelete("{projId}/languages/{langId}", Name ="DeleteProjectLanguage")]
        public async Task<IActionResult> DeleteProjectLanguage(int projId, int langId)
        {
            var success = await service.TryRemoveProjectLanguage(projId, langId);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        // Get: Projects/5/complexString
        [HttpGet("{id}/complexStrings", Name = "GetProjectStrings")]
        public async Task<IActionResult> GetProjectStrings(int id)
        {
            var projectsStrings = await service.GetProjectStringsAsync(id);
            return projectsStrings == null ? NotFound("No project strings found!") as IActionResult
                : Ok(projectsStrings);
        }

        // POST: Projects
        [HttpPost]
        public async Task<IActionResult> AddProject( IFormFile formFile)
        {

            Request.Form.TryGetValue("project", out StringValues res);

            ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(res);

            if (Request.Form.Files.Count != 0)
            {
                IFormFile file = Request.Form.Files[0];
                byte[] byteArr;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    await file.CopyToAsync(ms);
                    byteArr = ms.ToArray();
                }

                project.ImageUrl = await fileStorageProvider.UploadFileAsync(byteArr, FileType.Photo, Path.GetExtension(file.FileName));
            }
            var entity = await service.PostAsync(project,UserIdentityService.GetCurrentUser().Id);
            return entity == null ? StatusCode(409) as IActionResult
                : Created($"{Request?.Scheme}://{Request?.Host}{Request?.Path}{entity.Id}",
                entity);
            
        }

        // PUT: Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProject(int id, IFormFile formFile)
        {
			Request.Form.TryGetValue("project", out StringValues res);

			ProjectDTO project = JsonConvert.DeserializeObject<ProjectDTO>(res);
			project.Id = id;

			if (Request.Form.Files.Count != 0)
			{
				IFormFile file = Request.Form.Files[0];
				byte[] byteArr;
				using (var ms = new MemoryStream())
				{
					file.CopyTo(ms);
					await file.CopyToAsync(ms);
					byteArr = ms.ToArray();
				}

				project.ImageUrl = await fileStorageProvider.UploadFileAsync(byteArr, FileType.Photo, Path.GetExtension(file.FileName));
			}

            var entity = await service.PutAsync(project);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var success = await service.TryDeleteAsync(id);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
		
		[HttpPost]
		[Route("{id}/dictionary")]
		public async Task<IActionResult> AddFileDictionary(int id, IFormFile files)
		{
			await service.FileParseDictionary(id, Request.Form.Files[0]);
			return Ok();
		}
	}
}
