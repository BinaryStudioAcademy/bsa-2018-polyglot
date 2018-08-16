namespace Polyglot.Common.DTOs
{
    public class ProjectLanguageDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }

        public int LanguageId { get; set; }
        public LanguageDTO Language { get; set; }
    }
}