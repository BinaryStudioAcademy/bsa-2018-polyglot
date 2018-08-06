using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
	public class RightRepository : Repository<Right>, IRightRepository
	{
		public RightRepository(DataContext c)
			: base(c)
		{

		}
    }
}
