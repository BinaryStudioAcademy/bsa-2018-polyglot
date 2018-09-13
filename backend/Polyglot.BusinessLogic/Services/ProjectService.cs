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
using Polyglot.DataAccess.MongoRepository;
using Polyglot.DataAccess.SqlRepository;
using System.Text;
using Castle.Core.Internal;
using Nest;
using Polyglot.DataAccess.Elasticsearch;
using Polyglot.Common.DTOs;
using Polyglot.Core.Authentication;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.MongoModels;
using ComplexString = Polyglot.DataAccess.MongoModels.ComplexString;
using Polyglot.Common.Helpers.SignalR;
using Polyglot.Core.SignalR.Responses;
using Polyglot.BusinessLogic.Interfaces.SignalR;
using Microsoft.AspNetCore.Authorization;
using Language = Polyglot.DataAccess.Entities.Language;
using Polyglot.DataAccess.Entities.Chat;
using Polyglot.Common.DTOs.Chat;
using KBCsv;

namespace Polyglot.BusinessLogic.Services
{
    [Authorize]
    public class ProjectService : CRUDService<Project, ProjectDTO>, IProjectService
    {
        private readonly IMongoRepository<DataAccess.MongoModels.ComplexString> stringsProvider;
        private readonly IMongoRepository<ProjectPriority> projPrioritiesProvider;
        public IFileStorageProvider fileStorageProvider;
        private readonly IComplexStringService stringService;
        private readonly ISignalRWorkspaceService signalrService;
        ICRUDService<UserProfile, UserProfileDTO> userService;
        private readonly IGlossaryService glossaryService;
        private readonly IElasticClient elasticClient;
        private readonly ICurrentUser currentUser;
        private static readonly Random rand = new Random();

        public ProjectService(IUnitOfWork uow, IMapper mapper, IMongoRepository<DataAccess.MongoModels.ComplexString> rep,
            IMongoRepository<ProjectPriority> priorityRep,
            IFileStorageProvider provider, IComplexStringService stringService, IUserService userService,
            ISignalRWorkspaceService signalrService, IGlossaryService glossaryService, IElasticClient elasticClient, ICurrentUser currentUser)
            : base(uow, mapper)
        {
            stringsProvider = rep;
            this.fileStorageProvider = provider;
            this.stringService = stringService;
            this.userService = userService;
            this.signalrService = signalrService;
            this.glossaryService = glossaryService;
            this.elasticClient = elasticClient;
            this.projPrioritiesProvider = priorityRep;
            this.currentUser = currentUser;
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

                case "application/vnd.ms-excel":
                case "text/csv":
                    using (var textReader = new StreamReader(file.OpenReadStream()))
                    using (var reader = new CsvReader(textReader, true))
                    {
                        while (reader.HasMoreRecords)
                        {
                            var dataRecord = await reader.ReadDataRecordAsync();

                            dictionary[dataRecord[0]] = dataRecord[1];
                        }
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
                        Tags = new List<int>(),
                        CreatedOn = DateTime.Now,
                        CreatedBy = (await currentUser.GetCurrentUserProfile()).Id
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

                case ".csv":
                    string tempCSV = "";
                    foreach (var c in targetStrings)
                    {
                        if (c.Translations.FirstOrDefault(x => x.LanguageId == languageId) != null)
                            tempCSV = String.Concat(tempCSV, $"\"{c.Key}\",\"{c.Translations.FirstOrDefault(x => x.LanguageId == languageId).TranslationValue}\"\n");
                    }
                    arr = Encoding.UTF8.GetBytes(tempCSV);
                    break;

                default:
                    throw new NotImplementedException();

            }

            return arr;

        }
        
        public async Task IncreasePriority(int projectId)
        {
            var currentUserId = (await currentUser.GetCurrentUserProfile())?.Id;
            if(!currentUserId.HasValue)
            {
                return;
            }

            var currentPriority = (await projPrioritiesProvider.GetAllAsync(pp => pp.UserId == currentUserId.Value))?.FirstOrDefault();
            if(currentPriority == null)
            {
                await projPrioritiesProvider.CreateAsync(new ProjectPriority()
                {
                    Id = rand.Next(Int32.MinValue, Int32.MaxValue - 1),
                    UserId = currentUserId.Value,
                    Total = 1,
                    Priorities = new List<Priority>() { new Priority() { ProjectId = projectId, PriorityValue = 1 } }
                });
            }
            else
            {
                var currentProjectPriority = currentPriority.Priorities.FirstOrDefault(p => p.ProjectId == projectId);
                if(currentProjectPriority == null)
                {
                    currentPriority.Priorities.Add(new Priority() { ProjectId = projectId, PriorityValue = 1 });
                }
                else
                {
                    currentProjectPriority.PriorityValue++;
                }

                currentPriority.Total++;
                await projPrioritiesProvider.Update(currentPriority);
            }
        }

        #region Teams

        public async Task<IEnumerable<TeamPrevDTO>> GetProjectTeams(int projectId)
        {
            var project = await uow.GetRepository<Project>()
                    .GetAsync(projectId);

            if (project == null)
                return null;

            var projectTeams = project.ProjectTeams;
            var teams = new List<Team>();

            projectTeams.ToList().ForEach(projectTeam => teams.Add(projectTeam.Team));

            return mapper.Map<IEnumerable<TeamPrevDTO>>(teams);
        }

        public async Task<ProjectDTO> AssignTeamsToProject(int projectId, int[] teamIds)
        {
            if (teamIds.Length < 1)
                return null;

            var teamsIdInDB = (await uow.GetRepository<ProjectTeam>().GetAllAsync(x => x.ProjectId == projectId))
                .Select(x => x.TeamId);

            var idForAdd = teamIds.Except(teamsIdInDB);
            var projectDialog = await uow.GetRepository<ChatDialog>()
                .GetAsync(d => d.DialogType == ChatGroup.chatProject && d.Identifier == projectId);
            List<Team> assignedTeams = new List<Team>();

            foreach (var item in idForAdd)
            {
                await uow.GetRepository<ProjectTeam>().CreateAsync(new ProjectTeam() { ProjectId = projectId, TeamId = item });
                assignedTeams.Add(await uow.GetRepository<Team>().GetAsync(item));
            }

            var participants = assignedTeams
                .SelectMany(t => t.TeamTranslators)
                .Select(tt => tt.UserProfile)
                .ToList();

            participants.AddRange(assignedTeams.Select(t => t.CreatedBy));

            participants = participants.GroupBy(up => up.Id)
                .SelectMany(g => g.Select(sg => sg))
                .Except(projectDialog.DialogParticipants.Select(dp => dp.Participant))
                .ToList();

            participants.ForEach(p =>
            {
                projectDialog.DialogParticipants.Add(new DialogParticipant()
                {
                    Participant = p
                });
            });

            await uow.GetRepository<ChatDialog>().Update(projectDialog);
            await uow.SaveAsync();

            var project = await uow.GetRepository<Project>().GetAsync(projectId);
            return project != null ? mapper.Map<ProjectDTO>(project) : null;
        }

        public async Task<bool> TryDismissProjectTeam(int projectId, int teamId)
        {
            var projectTeam = (await uow.GetRepository<ProjectTeam>()
                .GetAllAsync(x => x.ProjectId == projectId && x.TeamId == teamId))
                .FirstOrDefault();

            if (projectTeam == null)
                return false;

            await uow.GetRepository<ProjectTeam>().DeleteAsync(projectTeam.Id);

            return await uow.SaveAsync() > 0;
        }

        #endregion Teams

        #region Languages


        public async Task<IEnumerable<LanguageStatisticDTO>> GetProjectLanguagesStatistic(int id)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(id);
            if (proj != null && proj.ProjectLanguageses.Count > 0)
            {
                var langs = proj.ProjectLanguageses?
                    .Select(p => p.Language);

                return (await GetLanguagesStatistic(id, langs)) ?? null;
            }
            else
            {
                return null;
            }
        }

        //public async Task<IEnumerable<LanguageStatisticDTO>> GetProjectLanguagesStatistic(int projectId, int[] languageIds)
        //{
        //    var proj = await uow.GetRepository<Project>().GetAsync(projectId);
        //    if (proj != null && proj.ProjectLanguageses.Count > 0)
        //    {
        //        var langs = proj.ProjectLanguageses?
        //            .Select(p => p.Language)
        //            .Where(l => languageIds.Contains(l.Id));

        //        return (await GetLanguagesStatistic(projectId, langs)) ?? null;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task<LanguageStatisticDTO> GetProjectLanguageStatistic(int projectId, int langId)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(projectId);
            if (proj != null && proj.ProjectLanguageses.Count > 0)
            {
                var lang = proj.ProjectLanguageses
                    ?.Where(pl => pl.LanguageId == langId)
                    ?.Select(pl => pl.Language)
                    ?.FirstOrDefault();

                if (lang == null)
                    return null;

                return
                     (
                     await GetLanguagesStatistic(projectId, new List<Language>() { lang })
                     )
                     ?.FirstOrDefault() ?? null;
            }
            {
                return null;
            }
        }

        public async Task<IEnumerable<LanguageDTO>> GetProjectLanguages(int id)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(id);
            if (proj != null && proj.ProjectLanguageses.Count > 0)
            {
                var langs = proj.ProjectLanguageses?.Select(p => p.Language);
                if (langs != null)
                    return mapper.Map<IEnumerable<LanguageDTO>>(langs);
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

            await uow.GetRepository<Project>().Update(project);
            await uow.SaveAsync();

            await signalrService.LanguagesAdded($"{Group.project}{project.Id}", languageIds);
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

                if (targetProdLang != null
                    && project.ProjectLanguageses.Remove(targetProdLang)
                    && await uow.GetRepository<Project>().Update(project) != null
                    && (await uow.SaveAsync()) > 0)
                {
                    var projectStrings = await stringsProvider.GetAllAsync(cs => cs.ProjectId == project.Id);

                    for (int i = 0; i < projectStrings.Count; i++)
                    {
                        projectStrings[i].Translations =
                            projectStrings[i].Translations
                            ?.Except(projectStrings[i]
                            ?.Translations
                            ?.Where(t => t.LanguageId == languageId))
                            ?.ToList();

                        await stringsProvider.Update(projectStrings[i]);
                    }
                    await signalrService.LanguageRemoved($"{Group.project}{projectId}", languageId);
                    return true;
                }
            }
            return false;
        }

        #endregion Languages

        #region Project overrides

		public async Task<List<ProjectDTO>> SearchProjects(string query)
		{
			IEnumerable<ProjectDTO> all = await GetListAsync();

			List<ProjectDTO> selected = new List<ProjectDTO>();

			foreach(var pr in all)
			{
				if(pr.Name.Contains(query))
				{
					selected.Add(pr);
				}
			}

			return selected;
		}


        public override async Task<IEnumerable<ProjectDTO>> GetListAsync()
        {
            var user = await currentUser.GetCurrentUserProfile();
            List<Project> result = new List<Project>();
            if (user.UserRole == Role.Manager)
            {
                result = await uow.GetRepository<Project>().GetAllAsync(x => x.UserProfile.Id == user.Id);
            }
            else
            {
                var translatorTeams = await uow.GetRepository<TeamTranslator>().GetAllAsync(x => x.TranslatorId == user.Id);

                translatorTeams.ForEach(team => team.Team.ProjectTeams.ToList()
                    .ForEach(project => result.Add(project.Project)));

                result = result.Distinct().ToList();
            }

			var mapped = mapper.Map<List<ProjectDTO>>(result);
            var projectsPriority = (
                await projPrioritiesProvider.GetAllAsync(p => p.UserId == user.Id)
                )
                ?.FirstOrDefault();

            int? currentPriority = 0;
            // Add progress to DTO here
            foreach (var p in mapped)
			{
                currentPriority = projectsPriority?.Priorities.FirstOrDefault(pp => pp.ProjectId == p.Id)?.PriorityValue;
                if(currentPriority.HasValue && projectsPriority.Total > 0)
                {
                    p.Priority = (int)(((long)currentPriority.Value) * 100 / projectsPriority.Total);
                }

				List<ComplexString> temp = new List<ComplexString>();
                temp = await stringsProvider.GetAllAsync(str => str.ProjectId == p.Id);
                int languagesAmount = p.ProjectLanguageses.Count;
                int max = temp.Count * languagesAmount;
                int currentProgress = 0;
                foreach (var str in temp)
                {
                    currentProgress += str.Translations.Count;
                }
                if (currentProgress == 0 || max == 0)
                {
                    p.Progress = 0;
                }
                else
                {
                    p.Progress = Convert.ToInt32((Convert.ToDouble(currentProgress) / Convert.ToDouble(max)) * 100);
                }
            }
			return mapped;
        }

        public override async Task<ProjectDTO> PostAsync(ProjectDTO entity)
        {
            var ent = mapper.Map<Project>(entity);
            ent.MainLanguage = null;
            ent.UserProfile = await currentUser.GetCurrentUserProfile();

            var target = await uow.GetRepository<Project>().CreateAsync(ent);
            await uow.SaveAsync();

            var dialog = new ChatDialog()
            {
                DialogName = target.Name,
                DialogType = ChatGroup.chatProject,
                Identifier = target.Id,
                DialogParticipants = new List<DialogParticipant>() { new DialogParticipant() { Participant = target.UserProfile } }
            };

            await uow.GetRepository<ChatDialog>().CreateAsync(dialog);

            
            return mapper.Map<ProjectDTO>(target);
        }

        public override async Task<ProjectDTO> PutAsync(ProjectDTO entity)
        {
            var source = mapper.Map<Project>(entity);

            Project target = await uow.GetRepository<Project>().GetAsync(entity.Id);



            if (target.ImageUrl != null && source.ImageUrl != null)
            {
                try
                {
                    await fileStorageProvider.DeleteFileAsync(target.ImageUrl);
                }
                catch (Exception)
                {

                }
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

            target = await uow.GetRepository<Project>().Update(target);
            await uow.SaveAsync();


            return target != null ? mapper.Map<ProjectDTO>(target) : null;
        }

        public override async Task<bool> TryDeleteAsync(int identifier)
        {
            if (uow != null)
            {

                Project toDelete = await uow.GetRepository<Project>().GetAsync(identifier);
                try
                {
                    await fileStorageProvider.DeleteFileAsync(toDelete.ImageUrl);
                }
                catch (Exception)
                {

                }
                await stringsProvider.DeleteAll(str => str.ProjectId == identifier);
                await uow.GetRepository<Project>().DeleteAsync(identifier);
                var targetTeamDialogId = uow.GetRepository<ChatDialog>()
                .GetAsync(d => d.DialogType == ChatGroup.chatProject && d.Identifier == identifier)
                ?.Id;

                if (targetTeamDialogId.HasValue)
                {
                    await uow.GetRepository<ChatDialog>().DeleteAsync(targetTeamDialogId.Value);
                }
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
            var tags = await uow.GetRepository<Tag>().GetAllAsync();
            var map = mapper.Map<List<ComplexStringDTO>>(strings);

            for (int i = 0; i < map.Count; i++)
            {
                map[i].Tags = mapper.Map<List<TagDTO>>(tags.Where(x => strings[i].Tags.Contains(x.Id)));
            } 

            return map;
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetProjectStringsWithPaginationAsync(int id, int itemsOnPage, int page, string search)
        {
			// check if Elastic is connected
			// if true use elastic else use mongo			
			if (ElasticRepository.isElasticUsed)
			{
				// sorts string by creation date
				var sorter = new SortDescriptor<DataAccess.ElasticsearchModels.ComplexStringIndex>();
				sorter.Field("CreatedAt", Nest.SortOrder.Ascending);

				// ElasticSearch query equals to
				// ({id} == projectId) && (search == str.key || search == str.OriginalValue)
				// ProjectId MUST match and either Key or OriginalValue must match
				var result = await elasticClient.SearchAsync<DataAccess.ElasticsearchModels.ComplexStringIndex>(x => x
						.Sort(s => sorter)
						.From(page * itemsOnPage)
						.Size(itemsOnPage)
						.Query(q => q
							.Bool(b => b
								.Must(mu => mu
									.Match(ma => ma
										.Field(f => f.ProjectId.ToString())
										.Query(id.ToString())
									), mu => mu
									.Bool(bb => bb
										.MinimumShouldMatch(1)
										.Should(sh => sh
											.Match(m => m
												.Field( f=> f.Key)
												.Query(search)
											), sh => sh
											.Match(m => m
												.Field(f => f.OriginalValue)
												.Query(search)
											)
										)
									)
								)
							)						
						)							
					);
				// adding tags to dto`s
				var models = result.Documents.ToList();
				var tags = await uow.GetRepository<Tag>().GetAllAsync();
				var dtos = mapper.Map<List<ComplexStringDTO>>(models);
				for (int i = 0; i < dtos.Count; i++)
				{
					dtos[i].Tags = mapper.Map<List<TagDTO>>(tags.Where(x => models[i].Tags.Contains(x.Id)));
				}
				return dtos;
			}
			else
			{				
				var skipItems = itemsOnPage * page;
                var strings = await stringsProvider.GetAllAsync(x => x.ProjectId == id);
                var tags = await uow.GetRepository<Tag>().GetAllAsync();
                var map = mapper.Map<List<ComplexStringDTO>>(strings);
                for (int i = 0; i < map.Count; i++)
                {
                    map[i].Tags = mapper.Map<List<TagDTO>>(tags.Where(x => strings[i].Tags.Contains(x.Id)));
                }
                var paginatedStrings = map.OrderBy(x => x.Id).Skip(skipItems).Take(itemsOnPage);
                return mapper.Map<IEnumerable<ComplexStringDTO>>(paginatedStrings);
			}			
		}


        public async Task<IEnumerable<ComplexStringDTO>> GetListByFilterAsync(IEnumerable<string> options, int projectId)
        {
            List<FilterType> criteriaFilters = new List<FilterType>();
            List<string> tagFilters = new List<string>();
            List<ComplexStringDTO> result = new List<ComplexStringDTO>();

            foreach (var opt in options)
            {
                if (opt.StartsWith("filter/"))
                {
                    criteriaFilters.Add((FilterType)Enum.Parse(typeof(FilterType),
                        opt
                            .Replace("filter/", "")
                            .Replace(" ", string.Empty)));
                }
                else
                {
                    tagFilters.Add(opt.Replace("tags/",""));
                }
            }

            Expression<Func<ComplexString, bool>> finalFilter = x => x.ProjectId == projectId;

            if (criteriaFilters.Contains(FilterType.Translated))
                finalFilter = AndAlso(finalFilter, x => x.Translations.Count != 0);

            if (criteriaFilters.Contains(FilterType.Untranslated))
                finalFilter = AndAlso(finalFilter, x => x.Translations.Count == 0);

            if (criteriaFilters.Contains(FilterType.WithPhoto))
                finalFilter = AndAlso(finalFilter, x => x.PictureLink != null);

            var criteriaResult = await stringsProvider.GetAllAsync(finalFilter);


            var tags = await uow.GetRepository<Tag>().GetAllAsync();
            var mappedStrings = mapper.Map<List<ComplexStringDTO>>(criteriaResult);

            for (int i = 0; i < mappedStrings.Count; i++)
            {
                mappedStrings[i].Tags = mapper.Map<List<TagDTO>>(tags.Where(x => criteriaResult[i].Tags.Contains(x.Id)));
            }

            if (tagFilters.Count != 0)
            {
                foreach (var tag in tagFilters)
                {
                    foreach (var res in mappedStrings)
                    {
                        if (res.Tags.Select(x => x.Name).Contains(tag))
                            result.Add(res);
                    }
                }

            }
            else
            {
                result = mappedStrings;
            }

            return result;
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

			charts.Add(await GetTagStatistic(id));
			charts.Add(await GetTranslatorStatistic(id));
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
                var count = complexStrings.Count(cs => cs.Translations.Any(t => t.LanguageId == language.Id && !t.TranslationValue.IsNullOrEmpty()));
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
                var count = complexStrings.Count(cs => cs.Translations.All(t => t.LanguageId != language.Id || t.TranslationValue.IsNullOrEmpty()));
                chart1.Values.Add(new Point
                {
                    Name = language.Name,
                    Value = count
                });
            }
            return chart1;
        }
		public async Task<ChartDTO> GetTagStatistic(int id)
		{
			var complexStrings = await this.GetProjectStringsAsync(id);

			var chart = new ChartDTO
			{
				Name = "Count strings with different tags",
				Values = new List<Point>()
			};

			List<Point> points = new List<Point>();
			foreach (var complexString in complexStrings)
			{
				foreach (var tag in complexString.Tags)
				{
					var point = points.FirstOrDefault(p => p.Name == tag.Name);
					if (point == null)
					{
						points.Add(new Point() { Name = tag.Name, Value = 1 });

					}
					else
					{
						points.ForEach(p => {
							if (p.Name == tag.Name)
							{
								p.Value++;
							}
						});
					}
				}
			}
			chart.Values.AddRange(points);
			if (points.Count > 0)
			{
				chart.MaxValue = points.Max(p => p.Value);
			}
			return chart;
		}

		public async Task<ChartDTO> GetTranslatorStatistic(int id)
		{
			var complexStrings = (await this.GetProjectStringsAsync(id)).ToList();
			

			var chart = new ChartDTO
			{
				Name = "Count translations by translator",
				Values = new List<Point>()
			};

			List<Point> points = new List<Point>();
			foreach (var complexString in complexStrings)
			{
				for (int i = 0; i < complexString.Translations.Count(); i++)
				{
					complexString.Translations[i].AssignedTranslatorName = (await userService.GetOneAsync(complexString.Translations[i].AssignedTranslatorId)).FullName;
				}
				foreach (var translation in complexString.Translations)
				{
					var point = points.FirstOrDefault(p => p.Name == translation.AssignedTranslatorName);
					if (point == null && translation.AssignedTranslatorId!=0)
					{
						points.Add(new Point() { Name = translation.AssignedTranslatorName, Value = 1 });
					}
					else if(point != null)
					{
						points.ForEach(p => {
							if (p.Name == translation.AssignedTranslatorName)
							{
								p.Value++;
							}
						});
					}
				}
			}
			chart.Values.AddRange(points);
			if (points.Count > 0)
			{
				chart.MaxValue = points.Max(p => p.Value);
			} 
			return chart;
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
            WithPhoto
        }

        #endregion

        #region Glossary

        public async Task<ProjectDTO> AssignGlossaries(int projectId, int[] glossaryIds)
        {
            if (glossaryIds.Length < 1)
                return null;
            var project = await uow.GetRepository<Project>().GetAsync(projectId);
            if (project == null)
                return null;

            var glossaryRepo = uow.GetRepository<Glossary>();
            Glossary currentGlossary;
            glossaryIds = glossaryIds.Except(project.ProjectGlossaries?.Select(pl => pl.Glossary.Id)).ToArray();

            if (glossaryIds.Length < 1)
                return null;

            foreach (var glossaryId in glossaryIds)
            {
                currentGlossary = await glossaryRepo.GetAsync(glossaryId);
                if (currentGlossary != null)
                {
                    project.ProjectGlossaries.Add(new ProjectGlossary()
                    {
                        Glossary = currentGlossary
                    });
                }
            }

            if (project.ProjectGlossaries.Count < 1)
                return null;

            await uow.GetRepository<Project>().Update(project);
            await uow.SaveAsync();
            return mapper.Map<ProjectDTO>(project);
        }

        public async Task<IEnumerable<GlossaryDTO>> GetAssignedGlossaries(int projectId)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(projectId);
            if (proj != null)
            {
                var glossaries = proj.ProjectGlossaries?.Select(p => p.Glossary);
                return mapper.Map<IEnumerable<Glossary>, IEnumerable<GlossaryDTO>>(glossaries);
            }
            return null;
        }

        public async Task<IEnumerable<GlossaryDTO>> GetNotAssignedGlossaries(int projectId)
        {
            var assignedGlossaries = (await uow.GetMidRepository<ProjectGlossary>().GetAllAsync(g => g.ProjectId == projectId))
                .Select(x => x.GlossaryId)
                .ToList();
            var allGlossaries = await this.glossaryService.GetListAsync();
            return allGlossaries?.Where(g => !assignedGlossaries.Contains(g.Id));
        }

        public async Task<bool> TryDismissGlossary(int projectId, int glossaryId)
        {
            var project = await uow.GetRepository<Project>().GetAsync(projectId);

            if (project != null)
            {
                var targetProdGlossary = project.ProjectGlossaries
                    .Where(pl => pl.GlossaryId == glossaryId)
                    .FirstOrDefault();

                if (targetProdGlossary != null)
                    if (project.ProjectGlossaries.Remove(targetProdGlossary))
                        if (await uow.GetRepository<Project>().Update(project) != null)
                            return await uow.SaveAsync() > 0;
            }
            return false;
        }

        #endregion

        #region private members

        private async Task<IEnumerable<LanguageStatisticDTO>> GetLanguagesStatistic(int projectId, IEnumerable<Language> targetLanguages)
        {
            if (targetLanguages.Count() < 1)
                return null;

            var projectStrings = await stringsProvider.GetAllAsync(x => x.ProjectId == projectId);
            // если строк для перевода нет тогда ничего вычислять не нужно
            if (projectStrings.Count() < 1)
                return mapper.Map<IEnumerable<LanguageStatisticDTO>>(targetLanguages);

            // мапим языки проекта, а затем добавляем TranslatedStrings и ComplexStringsCount по каждому языку
            return mapper.Map<IEnumerable<Language>, IEnumerable<LanguageStatisticDTO>>(targetLanguages, opt => opt.AfterMap((src, dest) =>
            {
                var languageDTOs = dest.ToList();
                int? translatedCount = 0;

                for (int i = 0; i < languageDTOs.Count; i++)
                {
                    // ищем переводы по каждому языку
                    translatedCount = projectStrings?.Count(complexString =>
                        complexString.Translations.Any(x => x.LanguageId == languageDTOs[i].Id));

                    languageDTOs[i].ComplexStringsCount = projectStrings.Count();

                    if (translatedCount.HasValue && translatedCount.Value > 0)
                    {
                        languageDTOs[i].TranslatedStringsCount = translatedCount.Value;
                    }
                }
            }));
        }

        #endregion private members

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
                    DateTime = projectString.CreatedOn
                });

                var comments = await this.stringService.GetCommentsAsync(projectString.Id);

                foreach (var comment in comments)
                {
                    allActivities.Add(new ActivityDTO()
                    {
                        Message = $"New comment was added by {comment.User.FullName} in string with key {projectString.Key}",
                        DateTime = comment.CreatedOn,
                        User = comment.User
                    });
                }

                var translations = await this.stringService.GetStringTranslationsAsync(projectString.Id);
                foreach (var translation in translations)
                {
                    var user = await this.userService.GetOneAsync(translation.UserId);
                    allActivities.Add(new ActivityDTO()
                    {
                        Message = $"New translation was added in string with key {projectString.Key} by {user.FullName}",
                        DateTime = translation.CreatedOn,
                        User = user
                    });
                    foreach (var trans in translation.History)
                    {
                        var user2 = await this.userService.GetOneAsync(trans.UserId);
                        allActivities.Add(new ActivityDTO()
                        {
                            Message = $"Translation in string with key {projectString.Key} was added by {user2.FullName}(obsolete)",
                            DateTime = trans.CreatedOn,
                            User = user2
                        });
                    }
                    foreach (var trans in translation.OptionalTranslations)
                    {
                        var user2 = await this.userService.GetOneAsync(trans.UserId);
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

        public async Task<ProjectTranslationStatisticsDTO> GetProjectLanguageStatistic(int projectId)
        {
            var proj = await uow.GetRepository<Project>().GetAsync(projectId);
            if (proj != null && proj.ProjectLanguageses.Count > 0)
            {
                var langs = proj.ProjectLanguageses?
                    .Select(p => p.Language).ToList();

                var projectStrings = await stringsProvider.GetAllAsync(x => x.ProjectId == projectId);
                var totalTranslationsNeeded = projectStrings.Count * langs.Count();

                int totalTranslationsDone = 0;

                foreach (var language in langs)
                {
                    totalTranslationsDone += projectStrings.Count(x => x.Translations.Any(y => y.LanguageId == language.Id));
                }

                var progress = totalTranslationsNeeded == 0 ? 0 : (100 / totalTranslationsNeeded) * totalTranslationsDone;

                return new ProjectTranslationStatisticsDTO
                {
                    ProjectId = projectId,
                    Progress = progress,
                    TotalComplexStringsTranslationCount = totalTranslationsNeeded,
                    TranslatedStringsCount = totalTranslationsDone
                };
            }

            return null;
        }

        public async Task<IEnumerable<ProjectTranslationStatisticsDTO>> GetProjectLanguageStatistics(List<int> projectIds)
        {
            var projectsStatistics = new List<ProjectTranslationStatisticsDTO>();
            foreach (var projectId in projectIds)
            {
                projectsStatistics.Add(await GetProjectLanguageStatistic(projectId));
            }

            return projectsStatistics;
        }
    }

}
