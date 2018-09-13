using System;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class AdditionalTranslationDTO
    {
        public Guid Id { get; set; }
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }

        public AdditionalTranslationDTO()
		{

		}
	}
}
