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

namespace Polyglot.BusinessLogic.Implementations
{
	public class ProjectService : IProjectService // , CRUDService<ProjectDTO, int>
	{
		IRepository<ComplexString> repository;

		public ProjectService()
		{
			
		}

		public async Task FileParse(IFormFile file)
		{
			try
			{
				string str = String.Empty;

				using (var reader = new StreamReader(file.OpenReadStream()))
				{
					str = reader.ReadToEnd();
				}

				ComplexString c = JsonConvert.DeserializeObject<ComplexString>(str);

				// repository isn`t working now
				// await repository.Add(c);
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		public async Task FileParseDictionary(IFormFile file)
		{

			try
			{
				Dictionary<string, string> dictionary;

				string str = String.Empty;

				using (var reader = new StreamReader(file.OpenReadStream()))
				{
					str = reader.ReadToEnd();
				}

				dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);

				foreach(var i in dictionary)
				{
					ComplexString temp = new ComplexString() { Key = i.Key, OriginalValue = i.Value };
					// repository isn`t working now
					// await repository.Add(new ComplexString() { Key = i.Key, OriginalValue = i.Value });
				}

			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}
}
