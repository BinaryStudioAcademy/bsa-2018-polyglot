namespace Polyglot.Common.DTOs
{
    public class ProjectTranslationStatisticsDTO
    {
        public int ProjectId { get; set; }

        public int TranslatedStringsCount { get; set; }

        public int TotalComplexStringsTranslationCount { get; set; }
        public int Progress { get; set; }
    }
}