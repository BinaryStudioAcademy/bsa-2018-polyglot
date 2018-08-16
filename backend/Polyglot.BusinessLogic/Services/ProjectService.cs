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

namespace Polyglot.BusinessLogic.Services
{
    public class ProjectService : CRUDService<Project,ProjectDTO>, IProjectService
    {
        private readonly IMongoRepository<DataAccess.MongoModels.ComplexString> stringsProvider;
		private IUnitOfWork uow;
        public ProjectService(IUnitOfWork uow, IMapper mapper, IMongoRepository<DataAccess.MongoModels.ComplexString> rep)
            : base(uow, mapper)
        {
            stringsProvider = rep;
			this.uow = uow;

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

        public async Task<IEnumerable<ProjectDTO>> GetListAsync(int userId) => 
            mapper.Map<List<ProjectDTO>>(await Filtration<Project>(x => x.Manager.UserProfile.Id == userId));

        public override async Task<ProjectDTO> PostAsync(ProjectDTO entity)
		{			
			var ent = mapper.Map<Project>(entity);
			// ent.MainLanguage = await uow.GetRepository<Language>().GetAsync(entity.MainLanguage.Id);
			ent.MainLanguage = null;

			var target = await uow.GetRepository<Project>().CreateAsync(ent);
			await uow.SaveAsync();

			return mapper.Map<ProjectDTO>(target);			
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
