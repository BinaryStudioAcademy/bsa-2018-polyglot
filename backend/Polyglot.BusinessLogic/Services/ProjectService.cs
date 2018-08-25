﻿using System;
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
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;
using System.Text;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;

namespace Polyglot.BusinessLogic.Services
{
    public class ProjectService : CRUDService<Project, ProjectDTO>, IProjectService
    {
        private readonly IMongoRepository<DataAccess.MongoModels.ComplexString> stringsProvider;
        public IFileStorageProvider fileStorageProvider;
        private readonly IComplexStringService _stringService;
        ICRUDService<UserProfile, UserProfileDTO> _userService;


        public ProjectService(IUnitOfWork uow, IMapper mapper, IMongoRepository<DataAccess.MongoModels.ComplexString> rep,
            IFileStorageProvider provider, IComplexStringService stringService, IUserService userService)
            : base(uow, mapper)
        {
            stringsProvider = rep;
            this.fileStorageProvider = provider;
            this._stringService = stringService;
            this._userService = userService;
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
                await stringsProvider.CreateAsync(
                    new DataAccess.MongoModels.ComplexString()
                    {
                        Id = savedEntity.Id,
                        Key = i.Key,
                        OriginalValue = i.Value,
                        ProjectId = id,
                        Translations = new List<Translation>(),
                        Comments = new List<Comment>(),
                        Tags = new List<string>()
                    });
            }

        }

        public async Task<byte[]> GetFile(int id, int languageId, string format)
        {
            Language targetLanguage = await uow.GetRepository<Language>().GetAsync(languageId);
            Project targetProject = await uow.GetRepository<Project>().GetAsync(id);
            List<DataAccess.MongoModels.ComplexString> targetStrings = await stringsProvider.GetAllAsync(x => x.ProjectId == id);


            byte[] arr = null;

            Dictionary<string, string> myDictionary = new Dictionary<string, string>();


            switch (format)
            {
                case ".resx":
                    XDocument xdoc = new XDocument();
                    XElement root = new XElement("root");
                    foreach (var c in targetStrings)
                    {
                        if (c.Translations.FirstOrDefault(x => x.LanguageId == languageId) != null)
                        {
                            XElement key = new XElement("data");
                            XAttribute name = new XAttribute("name", c.Key);

                            key.Add(name);

                            XElement value = new XElement("value", c.Translations.FirstOrDefault(x => x.LanguageId == languageId).TranslationValue);

                            key.Add(value);

                            root.Add(key);
                        }
                    }
                    xdoc.Add(root);
                    string temp0 = xdoc.ToString();
                    arr = Encoding.UTF8.GetBytes(temp0);
                    break;
                case ".json":

                    foreach (var c in targetStrings)
                    {
                        if (c.Translations.FirstOrDefault(x => x.LanguageId == languageId) != null)
                            myDictionary.Add(c.Key, c.Translations.FirstOrDefault(x => x.LanguageId == languageId).TranslationValue);
                    }
                    string temp = JsonConvert.SerializeObject(myDictionary, Formatting.Indented);
                    arr = Encoding.UTF8.GetBytes(temp);
                    break;
                default:
                    throw new NotImplementedException();

            }

            return arr;

        }


        public override async Task<IEnumerable<ProjectDTO>> GetListAsync()
        {
            var user = await CurrentUser.GetCurrentUserProfile();
            return mapper.Map<List<ProjectDTO>>(await uow.GetRepository<Project>().GetAllAsync(x => x.UserProfile.Id == user.Id));
        }


        #region Teams

        public async Task<IEnumerable<TeamPrevDTO>> GetProjectTeams(int projectId)
        {
            var project = await uow.GetRepository<Project>()
                    .GetAsync(projectId);

            if (project == null)
                return null;

            var teams = project.Teams;
            return mapper.Map<IEnumerable<TeamPrevDTO>>(teams);
        }

        public async Task<ProjectDTO> AssignTeamsToProject(int projectId, int[] teamIds)
        {
            if (teamIds.Length < 1)
                return null;

            var project = await uow.GetRepository<Project>()
                .GetAsync(projectId);

            if (project == null)
                return null;

            var teamIdsToAdd = teamIds.Except(project.Teams.Select(t => t.Id));

            Team currentTeam;
            foreach (var id in teamIdsToAdd)
            {

                currentTeam = await uow.GetRepository<Team>()
                .GetAsync(id);
                project.Teams.Add(currentTeam);
            }

            project = uow.GetRepository<Project>()
                    .Update(project);
            await uow.SaveAsync();
            return project != null ? mapper.Map<ProjectDTO>(project) : null;

        }

        public async Task<bool> TryDismissProjectTeam(int projectId, int teamId)
        {
            var project = await uow.GetRepository<Project>()
                .GetAsync(projectId);

            if (project == null)
                return false;

            if (project.Teams?.Count() > 0)
            {
                var targetTeam = project.Teams.FirstOrDefault(t => t.Id == teamId);
                if (targetTeam == null)
                    return false;

                project.Teams.Remove(targetTeam);
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
                var langs = proj.ProjectLanguageses?.Select(p => p.Language);
                var projectStrings = await stringsProvider.GetAllAsync(x => x.ProjectId == id);
                // если строк для перевода нет тогда ничего вычислять не нужно
                if (projectStrings.Count() < 1)
                    return mapper.Map<IEnumerable<LanguageDTO>>(langs);

                var projectTranslations = projectStrings?.SelectMany(css => css.Translations).ToList();
                int percentUnit = (100 / projectStrings.Count());

                // мапим языки проекта, а затем вычисляем значения Progress и TranslationsCount по каждому языку
                return mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDTO>>(langs, opt => opt.AfterMap((src, dest) =>
                {
                    var languageDTOs = dest.ToList();
                    int progress = 0;
                    int? translatedCount = 0;

                    for (int i = 0; i < languageDTOs.Count; i++)
                    {
                        // ищем переводы по каждому языку
                        translatedCount = projectTranslations
                            ?.Where(t => t.LanguageId == languageDTOs[i].Id && !String.IsNullOrWhiteSpace(t.TranslationValue))
                            ?.Count();

                        if (!translatedCount.HasValue || translatedCount.Value < 1)
                            continue;

                        progress = translatedCount.Value * percentUnit;

                        languageDTOs[i].TranslationsCount = translatedCount.Value;
                        languageDTOs[i].Progress = progress;

                    }
                }));
            }
            return null;
        }

        public async Task<ProjectDTO> AddLanguagesToProject(int projectId, int[] languageIds)
        {
            if (languageIds.Length < 1)
                return null;
            var project = await uow.GetRepository<Project>().GetAsync(projectId);
            if (project == null)
                return null;

            var langsRepo = uow.GetRepository<Language>();
            Language currentLanguage;
            languageIds = languageIds.Except(project.ProjectLanguageses?.Select(pl => pl.Language.Id)).ToArray();

            if (languageIds.Length < 1)
                return null;

            foreach (var langId in languageIds)
            {
                currentLanguage = await langsRepo.GetAsync(langId);
                if (currentLanguage != null)
                {
                    project.ProjectLanguageses.Add(new ProjectLanguage()
                    {
                        Language = currentLanguage
                    });
                }
            }

            if (project.ProjectLanguageses.Count < 1)
                return null;

            uow.GetRepository<Project>().Update(project);
            await uow.SaveAsync();
            return mapper.Map<ProjectDTO>(project);
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
            //var managerDto = mapper.Map<UserProfileDTO>(await CurrentUser.GetCurrentUserProfile());
            //entity.UserProfile = managerDto;
            var ent = mapper.Map<Project>(entity);
            // ent.MainLanguage = await uow.GetRepository<Language>().GetAsync(entity.MainLanguage.Id);
            ent.MainLanguage = null;
            ent.UserProfile = await CurrentUser.GetCurrentUserProfile();

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


        public override async Task<bool> TryDeleteAsync(int identifier)
        {
            if (uow != null)
            {

                Project toDelete = await uow.GetRepository<Project>().GetAsync(identifier);
                if (toDelete.ImageUrl != null)
                    await fileStorageProvider.DeleteFileAsync(toDelete.ImageUrl);

                await uow.GetRepository<Project>().DeleteAsync(identifier);
                await uow.SaveAsync();
                return true;
            }
            else
                return false;
        }

        #endregion Project overrides

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

        public async Task<PaginatedStringsDTO> GetProjectStringsWithPaginationAsync(int id, int itemsOnPage, int page)
        {
            var skipItems = itemsOnPage * page;

            var strings = await stringsProvider.GetAllAsync(x => x.ProjectId == id);

            var paginatedStrings = strings.OrderBy(x => x.Id).Skip(skipItems).Take(itemsOnPage);

            var totalPages = (int)Math.Ceiling((double)(paginatedStrings?.Count() ?? 0) / itemsOnPage);

            return new PaginatedStringsDTO
            {
                TotalPages = totalPages,
                ComplexStrings = mapper.Map<IEnumerable<ComplexStringDTO>>(paginatedStrings)
            };

        }

        public async Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<string> options, int projectId)
        {
            List<FilterType> filters = new List<FilterType>();
            options.ToList().ForEach(x => filters.Add((FilterType)Enum.Parse(typeof(FilterType), x.Replace(" ", string.Empty))));

            Expression<Func<ComplexString, bool>> finalFilter = x => x.ProjectId == projectId;

            Expression<Func<ComplexString, bool>> translatedFilter = x => x.Translations.Count != 0;
            Expression<Func<ComplexString, bool>> untranslatedFilter = x => x.Translations.Count == 0;
            Expression<Func<ComplexString, bool>> withTagsFilter = x => x.Tags.Count != 0;
            Expression<Func<ComplexString, bool>> machineTranslationFilter = x => x.Translations.Any(t => t.Type == Translation.TranslationType.Machine);
            Expression<Func<ComplexString, bool>> humanTranslationFilter = x => x.Translations.Any(t => t.Type == Translation.TranslationType.Machine);

            if (filters.Contains(FilterType.Translated))
                finalFilter = AndAlso(finalFilter, translatedFilter);

            if (filters.Contains(FilterType.Untranslated))
                finalFilter = AndAlso(finalFilter, untranslatedFilter);

            if (filters.Contains(FilterType.HumanTranslation))
                finalFilter = AndAlso(finalFilter, humanTranslationFilter);

            if (filters.Contains(FilterType.MachineTranslation))
                finalFilter = AndAlso(finalFilter, machineTranslationFilter);

            if (filters.Contains(FilterType.WithTags))
                finalFilter = AndAlso(finalFilter, withTagsFilter);

            var result = await stringsProvider.GetAllAsync(finalFilter);

            return mapper.Map<IEnumerable<ComplexStringDTO>>(result);
        }

        private Expression<Func<T, bool>> AndAlso<T>(
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new Filter.ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new Filter.ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        #region Statistic

        public async Task<ProjectStatisticDTO> GetProjectStatistic(int id)
        {
            var charts = new List<ChartDTO>();
            var chart1 = await GetTranskatedStringToLanguagesStatistic(id);
            var chart2 = await GetNotTranskatedStringToLanguagesStatistic(id);

            charts.Add(chart1);
            charts.Add(chart2);

            return new ProjectStatisticDTO
            {
                Charts = charts
            };
        }

        public async Task<ChartDTO> GetTranskatedStringToLanguagesStatistic(int id)
        {
            var complexStrings = (await stringsProvider.GetAllAsync()).Where(x => x.ProjectId == id).ToList();
            var languages = await GetProjectLanuagesData(id);

            var chart1 = new ChartDTO
            {
                Name = "Translated strings",
                Values = new List<Point>()
            };

            foreach (var language in languages)
            {
                var count = complexStrings.Count(cs => cs.Translations.Any(t => t.LanguageId == language.Id));
                chart1.Values.Add(new Point
                {
                    Name = language.Name,
                    Value = count
                });
            }
            return chart1;
        }

        public async Task<ChartDTO> GetNotTranskatedStringToLanguagesStatistic(int id)
        {
            var complexStrings = (await stringsProvider.GetAllAsync()).Where(x => x.ProjectId == id).ToList();
            var languages = await GetProjectLanuagesData(id);

            var chart1 = new ChartDTO
            {
                Name = "Not translated strings",
                Values = new List<Point>()
            };

            foreach (var language in languages)
            {
                var count = complexStrings.Count(cs => cs.Translations.All(t => t.LanguageId != language.Id));
                chart1.Values.Add(new Point
                {
                    Name = language.Name,
                    Value = count
                });
            }
            return chart1;
        }

        private async Task<List<Language>> GetProjectLanuagesData(int id)
        {
            var languages = await uow.GetRepository<Language>().GetAllAsync();
            var projectLanguages = (await uow.GetRepository<Project>().GetAsync(id)).ProjectLanguageses;

            var projectLanguagesData = from lang in languages
                                       join projectLanguage in projectLanguages on lang.Id equals projectLanguage.LanguageId
                                       where projectLanguage.ProjectId == id
                                       select new Language
                                       {
                                           Id = lang.Id,
                                           Code = lang.Code,
                                           Name = lang.Name,
                                       };

            return projectLanguagesData.ToList();
        }

        #endregion

        public enum FilterType
        {
            Translated,
            Untranslated,
            HumanTranslation,
            MachineTranslation,
            WithTags
        }

        #endregion

        public async Task<IEnumerable<ActivityDTO>> GetAllActivitiesByProjectId(int id)
        {
            List<ActivityDTO> allActivities = new List<ActivityDTO>();

            var projectStrings = await this.GetProjectStringsAsync(id);
            foreach (var projectString in projectStrings)
            {
                allActivities.Add(new ActivityDTO()
                {
                    Message = $"Complex string with key {projectString.Key}" +
                    $" was assigned to the project",
                    DateTime = DateTime.Now
                });

                var comments = await this._stringService.GetCommentsAsync(projectString.Id);

                foreach (var comment in comments)
                {
                    allActivities.Add(new ActivityDTO()
                    {
                        Message = $"New comment was added by {comment.User.FullName} in string with key {projectString.Key}",
                        DateTime = comment.CreatedOn,
                        User = comment.User
                    });
                }

                var translations = await this._stringService.GetStringTranslationsAsync(projectString.Id);
                foreach (var translation in translations)
                {
                    var user = await this._userService.GetOneAsync(translation.UserId);
                    allActivities.Add(new ActivityDTO()
                    {
                        Message = $"New translation was added in string with key {projectString.Key} by {user.FullName}",
                        DateTime = translation.CreatedOn,
                        User = user
                    });
                    foreach (var trans in translation.History)
                    {
                        var user2 = await this._userService.GetOneAsync(trans.UserId);
                        allActivities.Add(new ActivityDTO()
                        {
                            Message = $"Translation in string with key {projectString.Key} was added by {user2.FullName}(obsolete)",
                            DateTime = trans.CreatedOn,
                            User = user2
                        });
                    }
                    foreach (var trans in translation.OptionalTranslations)
                    {
                        var user2 = await this._userService.GetOneAsync(trans.UserId);
                        allActivities.Add(new ActivityDTO()
                        {
                            Message = $"Optional translation in string with key {projectString.Key} was added by {user2.FullName}",
                            DateTime = trans.CreatedOn,
                            User = user2
                        });
                    }
                }

            }

            var teams = await this.GetProjectTeams(id);

            foreach (var team in teams)
            {
                ActivityDTO activity = new ActivityDTO();
                activity.DateTime = DateTime.Now;
                if (team.Persons.Count == 1)
                {
                    activity.Message = $"Team with 1 person was assigned to the project";
                }
                else
                {
                    activity.Message = $"Team with {team.Persons.Count} people was assigned to the project";
                }
                allActivities.Add(activity);
            }
            return allActivities.OrderByDescending(act => act.DateTime);
        }
    }

}
