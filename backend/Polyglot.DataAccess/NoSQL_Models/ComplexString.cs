using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.NoSQL_Models
{
    public class ComplexString
    {
		public int ProjectId { get; set; }
		public string Language { get; set; }
		public string OriginalValue { get; set; }
		public string Description { get; set; }
		public string PictureLink { get; set; }

		List<Translation> Translations { get; set; }
		List<Comment> Comments { get; set; }
		List<string> Tags { get; set; }

		public ComplexString()
		{

		}
	}
}
