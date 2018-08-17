using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs.NoSQL
{
	public class TranslationDTO
	{
		public int LanguageId { get; set; }
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }

		public List<AdditionalTranslationDTO> History { get; set; }
		public List<AdditionalTranslationDTO> OptionalTranslations { get; set; }

		public TranslationDTO()
		{

		}
	}
}
