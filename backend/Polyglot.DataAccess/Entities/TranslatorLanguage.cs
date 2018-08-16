namespace Polyglot.DataAccess.Entities
{
    public class TranslatorLanguage
    {
        public int? TranslatorId { get; set; }
        public virtual Translator Translator { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public string Proficiency { get; set; }
    }
}
