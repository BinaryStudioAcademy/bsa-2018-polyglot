using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.Interfaces;
using Polyglot.DataAccess.Entities;

namespace Polyglot.DataAccess.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
		public UserProfileRepository(DataContext c)
			:base(c)
		{

		}
    }
}
