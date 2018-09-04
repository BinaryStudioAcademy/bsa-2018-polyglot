using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;
using System.Linq;
using Polyglot.DataAccess.Helpers;

namespace Polyglot.Controllers
{
    [Produces("application/json")]
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class ProjectsController : ControllerBase
	{
		private IProjectService service;
        public IFileStorageProvider fileStorageProvider;
        private readonly IRightService rightService;
		public ProjectsController(IProjectService projectService, IFileStorageProvider provider, IRightService rightService)
		{
			this.service = projectService;
            this.rightService = rightService;
            fileStorageProvider = provider;
		}


        // GET: Projects

		[HttpGet]
		public async Task<IActionResult> GetAllProjects()
		{
			var projects = await service.GetListAsync();
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

        // GET: Projects/:id?/teams
        [HttpGet("{id}/teams", Name = "GetProjectTeams")]
        public async Task<IActionResult> GetProjectTeams(int id)
        {
            var project = await service.GetProjectTeams(id);
            return project == null ? NotFound($"Project with id = {id} has got no assigned team!") as IActionResult
                : Ok(project);

        }

        // PUT: Projects/:id/teams/:id
        [HttpPut("{projectId}/teams")]
        public async Task<IActionResult> AssignTeamsToProject(int projectId, [FromBody]int[] teamIds)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var entity = await service.AssignTeamsToProject(projectId, teamIds);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        //DELETE: projects/:id/teams/:id
        [HttpDelete("{projId}/teams/{teamId}", Name = "DismissProjectTeam")]
        public async Task<IActionResult> DismissProjectTeam(int projId, int teamId)
        {
            var success = await service.TryDismissProjectTeam(projId, teamId);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }

        // GET: Projects/5/report
        [HttpGet("{id}/reports", Name = "GetProjectReport")]
        public async Task<IActionResult> GetProjectReport(int id)
        {
            var project = await service.GetProjectStatistic(id);
            return project == null ? NotFound($"Project with id = {id} not found!") as IActionResult
                : Ok(project);
        }
        
        // GET: Projects/5/languages/stat
        [HttpGet("{prodId}/languages/stat", Name = "GetProjectLanguagesStatistic")]
        public async Task<IActionResult> GetProjectLanguagesStatistic(int prodId)
        {
            var project = await service.GetProjectLanguagesStatistic(prodId);
            return project == null ? NotFound($"Project with id = {prodId} has got no languages statistic!") as IActionResult
                : Ok(project);

        }

        // GET: Projects/:projId/languages/:langId/stat
        [HttpGet("{projId}/languages/{langId}/stat", Name = "GetProjectLanguageStatistic")]
        public async Task<IActionResult> GetProjectLanguageStatistic(int projId, int langId)
        {
            var project = await service.GetProjectLanguageStatistic(projId, langId);
            return project == null ? NotFound($"Project with id = {projId} has got no language with id = {langId}!") as IActionResult
                : Ok(project);

        }

        // GET: Projects/5/languages
        [HttpGet("{id}/languages", Name = "GetProjectLanguages")]
        public async Task<IActionResult> GetProjectLanguages(int id)
        {
            var project = await service.GetProjectLanguages(id);
            return project == null ? NotFound($"Project with id = {id} has got no languages!") as IActionResult
                : Ok(project);
        }

        // PUT: Projects/:id/languages
        [HttpPut("{projectId}/languages")]
        public async Task<IActionResult> AddLanguagesToProject(int projectId, [FromBody]int[] languageIds)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var project = await service.AddLanguagesToProject(projectId, languageIds);
            if(project != null)
            {
                return Ok(project);
            }
            else
            {
                return StatusCode(304) as IActionResult;
            }
        }

        //DELETE: projects/:id/languages/:id
        [HttpDelete("{projId}/languages/{langId}", Name = "DeleteProjectLanguage")]
        public async Task<IActionResult> DeleteProjectLanguage(int projId, int langId)
        {
            var success = await service.TryRemoveProjectLanguage(projId, langId);

            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(304) as IActionResult;
            }
        }

        // Get: Projects/5/complexString
        [HttpGet("{id}/complexStrings", Name = "GetProjectStrings")]
        public async Task<IActionResult> GetProjectStrings(int id)
        {
            var projectsStrings = await service.GetProjectStringsAsync(id);
            return projectsStrings == null ? NotFound("No project strings found!") as IActionResult
                : Ok(projectsStrings);
        }

        // Get: Projects/5/complexString
       [HttpGet("{id}/paginatedStrings", Name = "GetProjectStringsWithPagination")]
	    public async Task<IActionResult> GetProjectStrings(int id, [FromQuery(Name = "itemsOnPage")] int itemsOnPage = 7, [FromQuery(Name = "page")] int page = 0)
	    {
            var projectsStrings = await service.GetProjectStringsWithPaginationAsync(id,itemsOnPage,page);

            return projectsStrings == null ? NotFound("No project strings found!") as IActionResult
             : Ok(projectsStrings);
        }

        // POST: Projects
        [HttpPost]
        public async Task<IActionResult> AddProject(IFormFile formFile)
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

            var langIds = project.ProjectLanguageses.Select(l => l.Id);
            var entity = await service.PostAsync(project);
            entity = await service.AddLanguagesToProject(entity.Id, langIds.ToArray());
           
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


        [HttpPost("{id}/filteredstring", Name = "GetComplexStringsByFilter")]
        public async Task<IActionResult> GetComplexStringsByFilter([FromBody]IEnumerable<string> options, int id)
        {
            var complexStrings = await service.GetListByFilterAsync(options, id);
            return complexStrings == null ? NotFound("No files found!") as IActionResult
                : Ok(complexStrings);
        }


        // GET: Projects/5/glossaries
        [HttpGet("{id}/glossaries")]
        public async Task<IActionResult> GetAssignedGlossaries(int id)
        {
            var glossaries = await service.GetAssignedGlossaries(id);
            return glossaries == null ? NotFound($"Project with id = {id} has got no glossaries!") as IActionResult
                : Ok(glossaries);

        }

        // PUT: Projects/:id/glossaries
        [HttpPut("{projectId}/glossaries")]
        public async Task<IActionResult> AssignGlossariesToProject(int projectId, [FromBody]int[] glossaryIds)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;
            var entity = await service.AssignGlossaries(projectId, glossaryIds);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        //DELETE: projects/:id/glossaries/:id
        [HttpDelete("{projId}/glossaries/{glossaryId}")]
        public async Task<IActionResult> DismissProjectGlossary(int projId, int glossaryId)
        {
            var success = await service.TryDismissGlossary(projId, glossaryId);
            return success ? Ok() : StatusCode(304) as IActionResult;
        }
        
        [HttpGet("{projectId}/activities")]
        public async Task<IActionResult> GetAllActivities(int projectId)
        {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var activities = await service.GetAllActivitiesByProjectId(projectId);
            return activities == null ? StatusCode(404) as IActionResult
                : Ok(activities);
        }

	    [HttpGet("{projectId}/statistics")]
	    public async Task<IActionResult> GetStatistics(int projectId)
	    {
	        var statistics = await service.GetProjectLanguageStatistic(projectId);
	        return statistics == null ? StatusCode(404) as IActionResult
	            : Ok(statistics);
        }

	    [HttpPost("statistics")]
	    public async Task<IActionResult> GetStatistics([FromBody]List<int> projectIds)
	    {
            if (!ModelState.IsValid)
                return BadRequest() as IActionResult;

            var statistics = await service.GetProjectLanguageStatistics(projectIds);
	        return statistics == null ? StatusCode(404) as IActionResult
	            : Ok(statistics);
        }

        [HttpGet]
        [Route("{id}/export")]
        public async Task<IActionResult> GetFile(int id, int langId, string extension)
        {
            var test = await service.GetFile(id, langId, extension);


            string ex;
            switch (extension)
            {
                case ".resx":
                    ex = "application/xml";
                    break;
                case ".json":
                    ex = "application/json";
                    break;
                default:
                    throw new NotImplementedException();
            }

            var temp = File(test, ex);
            return temp;
        }

        [HttpGet("{projectId}/right/{rightDefinition}")]
        public async Task<bool> CheckIfUserCan(int projectId, RightDefinition rightDefinition)
        {
            return await rightService.CheckIfCurrentUserCanInProject(rightDefinition, projectId);
        }

    }

}


