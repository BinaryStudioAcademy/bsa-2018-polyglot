using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
		public ProjectRepository(DataContext c)
			:base(c)
		{

		}
    }
}
