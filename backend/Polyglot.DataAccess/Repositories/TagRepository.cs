using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
	{
		public TagRepository(DataContext c)
			:base(c)
		{

		}
    }
}
