using System;
using System.Collections.Generic;
using System.Text;
using Polyglot.DataAccess.MongoModels;

namespace Polyglot.Common.DTOs.NoSQL
{
	public class TranslationDTO
	{
        public Guid Id { get; set; }
		public int LanguageId { get; set; }
		public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }
        public bool IsConfirmed { get; set; }
        public int AssignedTranslatorId { get; set; }
        public string AssignedTranslatorName { get; set; }
        public string AssignedTranslatorAvatarUrl { get; set; }

        public List<AdditionalTranslationDTO> History { get; set; }
		public List<AdditionalTranslationDTO> OptionalTranslations { get; set; }

		public TranslationDTO()
		{

		}

	}
}
