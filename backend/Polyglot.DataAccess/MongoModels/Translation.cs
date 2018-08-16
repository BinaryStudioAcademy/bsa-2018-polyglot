using System;
using System.Collections.Generic;

namespace Polyglot.DataAccess.MongoModels
{
    public class Translation
    {
        public Guid Id { get; set; }
        public string Language { get; set; }
        public string TranslationValue { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public List<AdditionalTranslation> History { get; set; }
        public List<AdditionalTranslation> OptionalTranslations { get; set; }

        public Translation()
        {

        }
    }
}
