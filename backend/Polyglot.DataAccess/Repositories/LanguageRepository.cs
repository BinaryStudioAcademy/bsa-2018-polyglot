using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
		public LanguageRepository(DataContext c)
			:base(c)
		{

		}
    }
}
