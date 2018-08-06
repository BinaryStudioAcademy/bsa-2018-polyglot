namespace Polyglot.Common.DTOs
{
    public class TranslatorLanguageDTO
    {
        public int TranslatorId { get; set; }
        public TranslatorDTO Translator { get; set; }

        public int LanguageId { get; set; }
        public LanguageDTO Language { get; set; }

        public string Proficiency { get; set; }
    }
}
