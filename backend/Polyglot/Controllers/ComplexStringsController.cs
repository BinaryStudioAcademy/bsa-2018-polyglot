using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.FileRepository;
using Polyglot.DataAccess.Interfaces;
using Polyglot.Hubs;
using Polyglot.Hubs.Helpers;

namespace Polyglot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplexStringsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IComplexStringService dataProvider;
        public IFileStorageProvider fileStorageProvider;
        private readonly ISignalrWorkspaceService signalrService;

        public ComplexStringsController(IComplexStringService dataProvider, IMapper mapper, IFileStorageProvider provider, ISignalrWorkspaceService signalrService)
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
            fileStorageProvider = provider;
            this.signalrService = signalrService;
        }

        // GET: ComplexStrings
        [HttpGet]
        public async Task<IActionResult> GetAllComplexStrings()
        {
            var complexStrings = await dataProvider.GetListAsync();
            return complexStrings == null ? NotFound("No files found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<ComplexStringDTO>>(complexStrings));
        }

        // GET: ComplexStrings/5
        [HttpGet("{id}", Name = "GetComplexString")]
        public async Task<IActionResult> GetComplexString(int id)
        {
            var complexString = await dataProvider.GetComplexString(id);
            return complexString == null ? NotFound($"ComplexString with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<ComplexStringDTO>(complexString));
        }

        // GET: ComplexStrings/5/translations
        [HttpGet("{id}/translations", Name = "GetComplexStringTranslations")]
        public async Task<IActionResult> GetComplexStringTranslations(int id)
        {
            var translation = await dataProvider.GetStringTranslationsAsync(id);
            return translation == null ? NotFound($"ComplexString with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<TranslationDTO>>(translation));
        }

        // PUT: ComplexStrings/5/translations
        [HttpPost("{id}/translations")]
        public async Task<IActionResult> SetStringTranslation(int id, [FromBody]TranslationDTO translation)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await CurrentUser.GetCurrentUserProfile();
            translation.UserId = user.Id;

            var entity = await dataProvider.SetStringTranslation(id, translation);


            if (entity != null)
            {
                var targetProjectId = (await dataProvider.GetComplexString(id)).ProjectId;
                await signalrService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", entity.LanguageId);
                await signalrService.ChangedTranslation($"{Group.complexString}{id}", entity);

                return Ok(entity);
            }
            else
            {
                return StatusCode(304) as IActionResult;
            }
        }

        [HttpPut("{id}/translations")]
        public async Task<IActionResult> EditStringTranslation(int id, [FromBody]TranslationDTO translation)
        {
            var user = await CurrentUser.GetCurrentUserProfile();
            translation.UserId = user.Id;

            var entity = await dataProvider.EditStringTranslation(id, translation);

            if (entity != null)
            {
                var targetProjectId = (await dataProvider.GetComplexString(id)).ProjectId;
                await signalrService.LanguageTranslationCommitted($"{Group.project}{targetProjectId}", entity.LanguageId);
                await signalrService.ChangedTranslation($"{Group.complexString}{id}", entity);

                return Ok(entity);
            }
            else
            {
                return StatusCode(304) as IActionResult;
            }
        }

		[HttpPost("{stringId}/{translationId}")]
		public async Task<IActionResult> AddOptionalTranslation(int stringId, Guid translationId, string value)
		{
			var entity = await dataProvider.AddOptionalTranslation(stringId, translationId, value);

			if(entity != null)
			{
				return Ok(entity);

				// TODO: SignalR
			}
			else
			{
				return StatusCode(304) as IActionResult;
			}
		}

		[HttpGet("{stringId}/{translationId}/optional")]
		public async Task<IActionResult> GetOptionalTranslations(int stringId, Guid translationId)
		{
			var result = await dataProvider.GetOptionalTranslations(stringId, translationId);

			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return StatusCode(404) as IActionResult;
			}
		}


		// POST: ComplexStrings
		[HttpPost]
        public async Task<IActionResult> AddComplexString(IFormFile formFile)
        {
            Request.Form.TryGetValue("str", out StringValues res);
            ComplexStringDTO complexString = JsonConvert.DeserializeObject<ComplexStringDTO>(res);
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

                complexString.PictureLink = await fileStorageProvider.UploadFileAsync(byteArr, FileType.Photo, Path.GetExtension(file.FileName));
            }
            var entity = await dataProvider.AddComplexString(complexString);
            if(entity != null)
            {
                await signalrService.ComplexStringAdded($"{Group.project}{entity.ProjectId}", entity.Id);
                return Ok(entity);
            }
            else
            {
                return StatusCode(409) as IActionResult;
            }
        }

        // PUT: ComplexStrings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyComplexString(int id, [FromBody]ComplexStringDTO complexString)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            complexString.Id = id;

            var entity = await dataProvider.ModifyComplexString(complexString);
            return entity == null ? StatusCode(304) as IActionResult
                : Ok(mapper.Map<ComplexStringDTO>(entity));
        }

        // DELETE: ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplexString(int id)
        {
            var targetProjectId = (await dataProvider.GetComplexString(id)).ProjectId;
            var success = await dataProvider.DeleteComplexString(id);

            if (success)
            {
                await this.signalrService.ComplexStringRemoved($"{Group.project}{targetProjectId}", id);
                return Ok();
            }
            else
            {
                return StatusCode(304);
            }
        }

        // GET: ComplexStrings/5/comments
        [HttpGet("{id}/comments", Name = "GetComplexStringComments")]
        public async Task<IActionResult> GetComplexStringComments(int id)
        {
            var comments = await dataProvider.GetCommentsAsync(id);
            return comments == null ? NotFound($"ComplexString with id = {id} not found!") as IActionResult
                : Ok(mapper.Map<IEnumerable<CommentDTO>>(comments));
        }
                             
        // POST: ComplexStrings/5/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> SetStringComment(int id, [FromBody]CommentDTO comment)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await dataProvider.SetComment(id, comment);
            
            if (entity != null)
            {
                await signalrService.CommentAdded($"{Group.complexString}{id}", entity);
            }

            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }

        // DELETE: ComplexStrings/5/comments
        [HttpDelete("{id}/comments/{commentId}")]
        public async Task<IActionResult> DeleteStringComment(int id, Guid commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await dataProvider.DeleteComment(id, commentId);

            if (entity != null)
            {
                await signalrService.CommentDeleted($"{Group.complexString}{id}", entity);
            }

            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }


        // PUT: ComplexStrings/5/comments
        [HttpPut("{id}/comments")]
        public async Task<IActionResult> EditStringComment(int id, [FromBody]CommentDTO comment)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await dataProvider.EditComment(id, comment);

            if (entity != null)
            {
                await signalrService.CommentEdited($"{Group.complexString}{id}", entity);
            }

            return entity == null ? StatusCode(304) as IActionResult
                : Ok(entity);
        }






        [HttpGet("{id}/history/{translationId}")]
        public async Task<IActionResult> GetHistory(int id, Guid translationId)
        {
            var response = await dataProvider.GetHistoryAsync(id, translationId);

            return response == null ? StatusCode(400) as IActionResult
                : Ok(response);
        }
    }
}
