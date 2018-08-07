namespace Polyglot.DataAccess.Entities
{
    public class TranslatorLanguage : Entity
    {
        public int TranslatorId { get; set; }
        public Translator Translator { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public string Proficiency { get; set; }
    }
}
