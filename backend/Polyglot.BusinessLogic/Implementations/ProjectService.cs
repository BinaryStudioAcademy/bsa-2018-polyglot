using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.NoSQL_Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.NoSQL_Repository;
using AutoMapper;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.NoSQL;

namespace Polyglot.BusinessLogic.Implementations
{
    public class ProjectService : CRUDService, IProjectService
	{
        IComplexStringRepository stringsProvider;
		public ProjectService(IUnitOfWork uow, IMapper mapper, IComplexStringRepository rep)
            :base(uow, mapper)
		{
            this.stringsProvider = rep;
		}
        

		public async Task FileParseDictionary(IFormFile file)
		{			
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string str = String.Empty;


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
					
					foreach (XElement data in doc.Element("root").Elements("data"))
					{
						dictionary[data.Attribute("name").Value] = data.Element("value").Value;
					}


					break;

				default:
						throw new NotImplementedException();
				}

				foreach(var i in dictionary)
				{
					ComplexString temp = new ComplexString() { Key = i.Key, OriginalValue = i.Value };

                // repository isn`t working now
                await stringsProvider.CreateAsync(new ComplexString() { Key = i.Key, OriginalValue = i.Value });
            }			

		}

        #region Projects


        public async Task<ProjectDTO> GetProjectAsync(int id)
        {
            if (uow != null)
            {
                var project = (await uow.GetRepository<Project>()
                    .Include(p => p.Manager.UserProfile)
                    .Include(p => p.MainLanguage)
                    .GetByAsync(p => p.Id == id)
                    )
                    .FirstOrDefault();
                if (project != null)
                    return mapper.Map<ProjectDTO>(project);
            }
            return null;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync()
        {
            if (uow != null)
            {
                var projects = await uow.GetRepository<Project>()
                    .Include(p => p.Manager.UserProfile)
                    .Include(p => p.MainLanguage)
                    .GetAllAsync();
                return mapper.Map<IEnumerable<ProjectDTO>>(projects);
            }
            else
                return null;
        }
        

        public async Task<ProjectDTO> AddProjectAsync(ProjectDTO project)
        {
            return await PostAsync<Project, ProjectDTO>(project) ?? null;
        }

        public async Task<ProjectDTO> ModifyProjectAsync(ProjectDTO project)
        {
            return await PutAsync<Project, ProjectDTO>(project) ?? null;
        }

        public async Task<bool> TryDeleteProjectAsync(int id)
        {
            return await TryDeleteAsync<Project>(id);
        }


        #endregion


        #region ComplexStrings

        public async Task<IEnumerable<ComplexStringDTO>> GetAllStringsAsync()
        {
            var strings = (await stringsProvider.GetAllAsync()).AsEnumerable();
            return mapper.Map<IEnumerable<ComplexStringDTO>>(strings);
        }

        public async Task<IEnumerable<ComplexStringDTO>> GetProjectStringsAsync(int id)
        {
            var strings = await stringsProvider.GetAllByProjectIdAsync(id);
            return mapper.Map<IEnumerable<ComplexStringDTO>>(strings);
        }

        #endregion
    }
}
