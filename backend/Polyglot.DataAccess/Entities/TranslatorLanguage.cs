namespace Polyglot.DataAccess.Entities
{
    public class TranslatorLanguage : Entity
    {
        public int? TranslatorId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public string Proficiency { get; set; }
    }
}
