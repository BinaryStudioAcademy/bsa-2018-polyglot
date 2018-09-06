using System;

namespace Polyglot.DataAccess.MongoModels
{
    public class AdditionalTranslation
    {
        public Guid Id { get; set; } 
        public string TranslationValue { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedOn { get; set; }

        public AdditionalTranslation()
		{

		}
	}
}
