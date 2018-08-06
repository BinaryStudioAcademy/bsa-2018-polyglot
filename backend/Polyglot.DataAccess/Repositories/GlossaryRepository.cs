using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class GlossaryRepository : Repository<Glossary>, IGlossaryRepository
    {
		public GlossaryRepository(DataContext c)
			:base(c)
		{

		}
    }
}
