using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
		public FileRepository(DataContext c)
			:base(c)
		{

		}
    }
}
