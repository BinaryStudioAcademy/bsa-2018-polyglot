namespace Polyglot.Common.DTOs
{
    public class LanguageDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Progress { get; set; }

        public int TranslationsCount { get; set; }

        public int StringsCount { get; set; }

        public int TranslatedStrings { get; set; }

        public int NotTranslatedStrings { get; set; }
    }
}
