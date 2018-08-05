using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.NoSQL_Models
{
    public class Comment
    {
		public int UserId { get; set; }
		public string Text { get; set; }
		public DateTime CreatedOn { get; set; }

		public Comment()
		{

		}
	}
}
