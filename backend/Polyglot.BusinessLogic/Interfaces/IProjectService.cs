using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polyglot.Common.DTOs;
using Microsoft.AspNetCore.Http;



namespace Polyglot.BusinessLogic.Interfaces
{
    public interface IProjectService // : ICRUDService<ProjectDTO,int>
    {
		

		Task FileParseDictionary(IFormFile file);
	}
}
