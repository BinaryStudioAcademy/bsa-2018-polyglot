using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
	public class RatingRepository : Repository<Rating>, IRatingRepository
    {
		public RatingRepository(DataContext c)
			:base(c)
		{

		}
    }
}
