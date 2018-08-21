using System;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.Common.DTOs.NoSQL
{
    public class AdditionalTranslationDTO
    {
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }
        public Translation.TranslationType Type { get; set; }

        public AdditionalTranslationDTO()
		{

		}
	}
}
