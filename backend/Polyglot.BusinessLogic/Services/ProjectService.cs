using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Implementations;
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
        private readonly IMongoRepository<ComplexString> stringsProvider;
        public ProjectService(IUnitOfWork uow, IMapper mapper, IMongoRepository<ComplexString> rep)
            : base(uow, mapper)
        {
            stringsProvider = rep;
        }


        public async Task FileParseDictionary(IFormFile file)
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
                ComplexString temp = new ComplexString() { Key = i.Key, OriginalValue = i.Value };

                // repository isn`t working now
                await stringsProvider.CreateAsync(new ComplexString() { Key = i.Key, OriginalValue = i.Value });
            }

        }


		
		public override async Task<ProjectDTO> PostAsync(ProjectDTO entity)
		{			
			var ent = mapper.Map<Project>(entity);
			// ent.MainLanguage = await uow.GetRepository<Language>().GetAsync(entity.MainLanguage.Id);
			ent.MainLanguage = null;

			var target = await uow.GetRepository<Project>().CreateAsync(ent);
			await uow.SaveAsync();

			return mapper.Map<ProjectDTO>(target);			
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
