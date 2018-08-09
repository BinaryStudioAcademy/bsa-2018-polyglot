namespace Polyglot.Common.DTOs
{
    public class ProjectGlossaryDTO
    {
        public int Id { get; set; }
        public int GlossaryId { get; set; }
        public GlossaryDTO Glossary { get; set; }

        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
