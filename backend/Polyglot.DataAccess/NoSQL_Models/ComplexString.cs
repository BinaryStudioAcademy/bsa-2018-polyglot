using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.NoSQL_Models
{
    public class ComplexString
    {
		public int Id { get; set; }

		public int ProjectId { get; set; }
		public string Language { get; set; }
		public string OriginalValue { get; set; }
		public string Description { get; set; }
		public string PictureLink { get; set; }

		public List<Translation> Translations { get; set; }
		public List<Comment> Comments { get; set; }
		public List<string> Tags { get; set; }

		public ComplexString()
		{

		}
	}
}
