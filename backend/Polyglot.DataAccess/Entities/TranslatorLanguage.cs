namespace Polyglot.DataAccess.Entities
{
    public class TranslatorLanguage : MidEntity
    {
        public int? TranslatorId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }

#warning заменить на Polyglot.DataAccess.Helpers.Proficiency
        public string Proficiency { get; set; }
    }
}
