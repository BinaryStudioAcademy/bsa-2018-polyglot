using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.MongoModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.MongoRepository;
using AutoMapper;
using Polyglot.Common.DTOs;
using Polyglot.Common.DTOs.NoSQL;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic.Implementations
{
    public class ProjectService : CRUDService, IProjectService
    {
        IRepository<ComplexString> stringsProvider;
        public ProjectService(IUnitOfWork uow, IMapper mapper, IRepository<ComplexString> rep)
            : base(uow, mapper)
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

            foreach (var i in dictionary)
            {
                ComplexString temp = new ComplexString() { Key = i.Key, OriginalValue = i.Value };

                // repository isn`t working now
                await stringsProvider.CreateAsync(new ComplexString() { Key = i.Key, OriginalValue = i.Value });
            }

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
