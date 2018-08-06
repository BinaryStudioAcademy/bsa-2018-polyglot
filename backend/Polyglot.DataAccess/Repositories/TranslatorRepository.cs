using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class TranslatorRepository : Repository<Translator>, ITranslatorRepository
    {
		public TranslatorRepository(DataContext c)
			: base(c)
		{

		}
    }
}
