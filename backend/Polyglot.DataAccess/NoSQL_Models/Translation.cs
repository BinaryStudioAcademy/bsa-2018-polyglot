using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.NoSQL_Models
{
	public class Translation
	{
		public string Language { get; set; }
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }

		List<AdditionalTranslation> History { get; set; }
		List<AdditionalTranslation> OptionalTranslations { get; set; }

		public Translation()
		{

		}
	}
}
