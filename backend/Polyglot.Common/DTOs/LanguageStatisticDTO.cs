namespace Polyglot.Common.DTOs
{
    public class LanguageStatisticDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int TranslatedStringsCount { get; set; }

        public int ComplexStringsCount { get; set; }
    }
}
