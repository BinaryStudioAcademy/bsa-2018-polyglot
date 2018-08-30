using System;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class AdditionalTranslationDTO
    {
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }

        public AdditionalTranslationDTO()
		{

		}
	}
}
