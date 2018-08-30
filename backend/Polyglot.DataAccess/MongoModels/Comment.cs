using Polyglot.DataAccess.Entities;
using System;

namespace Polyglot.DataAccess.MongoModels
{
    public class Comment 
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
		public string Text { get; set; }
		public DateTime CreatedOn { get; set; }

		public Comment()
		{

		}
	}
}
