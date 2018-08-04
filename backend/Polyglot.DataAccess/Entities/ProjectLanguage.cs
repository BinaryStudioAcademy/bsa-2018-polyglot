namespace Polyglot.DataAccess.Entities
{
    public class ProjectLanguage
    {
        public int PorjectId { get; set; }
        public Project Project { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
