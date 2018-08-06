using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
	{
		public ManagerRepository(DataContext c)
			: base(c)
		{

		}
	}
}
