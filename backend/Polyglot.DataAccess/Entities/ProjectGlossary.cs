namespace Polyglot.DataAccess.Entities
{
    public class ProjectGlossary : Entity
    {
        public int GlossaryId { get; set; }
        public Glossary Glossary { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
