using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.Common.Mapping;
using Polyglot.DataAccess.NoSQL_Models;
using Polyglot.DataAccess.NoSQL_Repository;
using System.IO;
using Newtonsoft.Json;


using System.Xml;
using System.Xml.Linq;
using Polyglot.DataAccess;
using MongoDB.Driver;

namespace Polyglot.BusinessLogic.Implementations
{
	public class ProjectService : IProjectService // , CRUDService<ProjectDTO, int>
	{
		private IRepository<ComplexString> repository;
        
		public ProjectService(IRepository<ComplexString> rep)
		{
			this.repository = rep;
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
							str = reader.ReadToEnd();
						}
						dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
						break;

					case "text/xml":

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

					default:
						throw new NotImplementedException();
				}

				foreach(var i in dictionary)
				{
					ComplexString temp = new ComplexString() { Key = i.Key, OriginalValue = i.Value };
                // repository isn`t working now
                await repository.Add(new ComplexString() { Key = i.Key, OriginalValue = i.Value });
            }			
		}
	}
}
