using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class TranslationRepository :Repository<Translation>, ITranslationRepository
    {
		public TranslationRepository(DataContext c)
			: base(c)
		{

		}
    }
}
