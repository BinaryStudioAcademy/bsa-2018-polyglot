using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.MongoModels;
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;

using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.FileRepository;

using Polyglot.DataAccess.Interfaces;
namespace Polyglot.BusinessLogic.Services
{
    public class ProjectService : CRUDService<Project,ProjectDTO>, IProjectService
    {
        private readonly IMongoRepository<DataAccess.MongoModels.ComplexString> stringsProvider;
		private IUnitOfWork uow;
		public IFileStorageProvider fileStorageProvider;

		public ProjectService(IUnitOfWork uow, IMapper mapper, IMongoRepository<DataAccess.MongoModels.ComplexString> rep,
			IFileStorageProvider provider)
            : base(uow, mapper)
        {
            stringsProvider = rep;
			this.uow = uow;
			this.fileStorageProvider = provider;

        }

        public async Task FileParseDictionary(int id, IFormFile file)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string str;


            switch (file.ContentType)
            {


                case "application/json":

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        str = await reader.ReadToEndAsync();
                    }
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                    break;

                /*



                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        str = reader.ReadToEnd();
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(str);						
                    XmlElement root = doc.DocumentElement;
                    XmlNodeList childnodes = root.SelectNodes("*");
                    foreach (XmlNode n in childnodes)
                    {							
                        dictionary[n.Name] = n.InnerXml;
                    }
                    break;
                    */

                case "application/xml":
                case "application/octet-stream":

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        str = await reader.ReadToEndAsync();
                    }
                    XDocument doc = XDocument.Parse(str);

                    foreach (XElement data in doc.Element("root")?.Elements("data"))
                    {
                        dictionary[data.Attribute("name").Value] = data.Element("value").Value;
                    }


                    break;

                default:
                    throw new NotImplementedException();
            }

            foreach (var i in dictionary)
            {			
				var sqlComplexString = new DataAccess.Entities.ComplexString()
				{
					TranslationKey = i.Key,
					ProjectId = id,
				};
				var savedEntity = await uow.GetRepository<Polyglot.DataAccess.Entities.ComplexString>().CreateAsync(sqlComplexString);
				await uow.SaveAsync();				
				await stringsProvider.CreateAsync(new DataAccess.MongoModels.ComplexString() { Id = savedEntity.Id, Key = i.Key, OriginalValue = i.Value, ProjectId = id });
            }

        }

        public async Task<IEnumerable<ProjectDTO>> GetListAsync(int userId)
        {
            var manager = await Filtration<Manager>(x => x.UserProfile.Id == userId);
            return mapper.Map<List<ProjectDTO>>(await Filtration<Project>(x => x.Manager.Id == manager.FirstOrDefault().Id));
        }

        #region Teams

        public async Task<TeamPrevDTO> GetProjectTeam(int id)
        {
            var project = await uow.GetRepository<Project>()
                    .GetAsync(id);

            var team = project?.Teams?.FirstOrDefault();
            return team != null ? mapper.Map<TeamPrevDTO>(team) : null;
        }

        public async Task<ProjectDTO> AssignTeamToProject(int projectId, int teamId)
        {
            var project = await uow.GetRepository<Project>()
                .GetAsync(projectId);

            if (project == null)
                return null;

            var team = await uow.GetRepository<Team>()
                .GetAsync(teamId);

            if (team == null)
                return null;

            project.Teams = new List<Team>() { team };

            project = uow.GetRepository<Project>()
                    .Update(project);
            await uow.SaveAsync();
            return project != null ? mapper.Map<ProjectDTO>(project) : null;

        }

        public async Task<bool> DismissProjectTeam(int projectId, int teamId)
        {
            var project = await uow.GetRepository<Project>()
                .GetAsync(projectId);

            if (project == null)
                return false;

            if(project.Teams?.Count() > 0)
            {
                project.Teams = new List<Team>();
                project = uow.GetRepository<Project>()
                    .Update(project);
                return await uow.SaveAsync() > 0 && project != null;
            }

            return false;
        }

        #endregion Teams

        #region Languages

        public async Task<IEnumerable<LanguageDTO>> GetProjectLanguages(int id)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(id);
            if (proj != null && proj.ProjectLanguageses.Count > 0)
            {
                var langs = proj.ProjectLanguageses
                    ?.Select(p => p.Language);


                var translations = (await stringsProvider.GetAllAsync(x => x.ProjectId == id)
                    )
                    ?.SelectMany(css => css.Translations).ToList();

                return mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(langs, opt => opt.AfterMap((src, dest) =>
                {
                    var dtos = dest.ToList();
                    List<Translation> langTranslations = null;
                    int? progress = 0;
                    int? translatedCount = 0;
                    int percentUnit = 0;

                    for (int i = 0; i < dtos.Count; i++)
                    {
                        langTranslations = translations
                            ?.Where(t => t.LanguageId == dtos[i].Id).ToList();

                        if (langTranslations?.Count() < 1)
                            continue;

                        percentUnit = (int)(100 / langTranslations.Count());

                        translatedCount = langTranslations?
                            .Where(t => !String.IsNullOrWhiteSpace(t.TranslationValue))
                            ?.Count();
                        progress = (translatedCount.HasValue ? translatedCount.Value : 0) * percentUnit;

                        dtos[i].TranslationsCount = translatedCount.HasValue ? translatedCount.Value : 0;
                        dtos[i].Progress = progress.HasValue ? progress.Value : 0;

                    }
                }));
            }
            return null;
        }

        public async Task<ProjectDTO> AddLanguageToProject(int projectId, int languageId)
        {
            var project = await uow.GetRepository<Project>().GetAsync(projectId);
            var language = await uow.GetRepository<Language>().GetAsync(languageId);

            if (project != null)
            {
                var languageExistInProject = project.ProjectLanguageses
                    .Select(pl => pl.Language)
                    .Where(l => l.Id == languageId)
                    .FirstOrDefault() != null;

                if (!languageExistInProject)
                {
                    project.ProjectLanguageses.Add(new ProjectLanguage()
                    {
                        Language = language
                    });

                    project = uow.GetRepository<Project>().Update(project);
                    await uow.SaveAsync();
                    return mapper.Map<ProjectDTO>(project);
                }
            }
            return null;
        }

        public async Task<bool> TryRemoveProjectLanguage(int projectId, int languageId)
        {
            var project = await uow.GetRepository<Project>().GetAsync(projectId);

            if (project != null)
            {
                var targetProdLang = project.ProjectLanguageses
                    .Where(pl => pl.LanguageId == languageId)
                    .FirstOrDefault();

                if (targetProdLang != null)
                    if (project.ProjectLanguageses.Remove(targetProdLang))
                        if (uow.GetRepository<Project>().Update(project) != null)
                            return await uow.SaveAsync() > 0;
            }
            return false;
        }

        #endregion Languages

        #region Project overrides

        public override async Task<ProjectDTO> PostAsync(ProjectDTO entity)
        {
            var ent = mapper.Map<Project>(entity);
            // ent.MainLanguage = await uow.GetRepository<Language>().GetAsync(entity.MainLanguage.Id);
            ent.MainLanguage = null;

            var target = await uow.GetRepository<Project>().CreateAsync(ent);
            await uow.SaveAsync();

            return mapper.Map<ProjectDTO>(target);
        }

        public override async Task<ProjectDTO> PutAsync(ProjectDTO entity)
        {
            var source = mapper.Map<Project>(entity);

            Project target = await uow.GetRepository<Project>().GetAsync(entity.Id);



            if (target.ImageUrl != null && source.ImageUrl != null)
            {
                await fileStorageProvider.DeleteFileAsync(target.ImageUrl);
            }
            if (source.ImageUrl != null)
            {
                target.ImageUrl = source.ImageUrl;
            }


            target.Name = source.Name;
            target.Description = source.Description;
            target.Technology = source.Technology;

            target.MainLanguage = null;
            target.MainLanguageId = source.MainLanguageId;

            target = uow.GetRepository<Project>().Update(target);
            await uow.SaveAsync();

            return target != null ? mapper.Map<ProjectDTO>(target) : null;
        }

        #endregion Project overrides

        public async Task<ProjectDTO> PostAsync(ProjectDTO entity, int userId)
        {
            var manager = await Filtration<Manager>(x => x.UserProfile.Id == userId);
            var managerDTO = mapper.Map<ManagerDTO>(manager.FirstOrDefault());
            entity.Manager = managerDTO;
            return await PostAsync(entity);
        }

        private async Task<IEnumerable<T>> Filtration<T>(Expression<Func<T, bool>> predicate) where T : Entity,new()
        {
            var result = await uow.GetRepository<T>().GetAllAsync(predicate);
            return result;
        }

        #region ComplexStrings

        public async Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync()
        {
            var strings = (await stringsProvider.GetAllAsync()).AsEnumerable();
            return mapper.Map<IEnumerable<ComplexStringDTO>>(strings);
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id)
        {
            var strings = await stringsProvider.GetAllAsync(x => x.ProjectId == id);
            return mapper.Map<IEnumerable<ComplexStringDTO>>(strings);
        }

        

        #endregion
    }
}
