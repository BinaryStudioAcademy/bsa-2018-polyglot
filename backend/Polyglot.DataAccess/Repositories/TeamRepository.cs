using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
		public TeamRepository(DataContext c) 
			: base(c)
		{

		}
    }
}
