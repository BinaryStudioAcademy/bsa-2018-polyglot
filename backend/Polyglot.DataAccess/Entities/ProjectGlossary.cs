namespace Polyglot.DataAccess.Entities
{
    public class ProjectGlossary 
    {
        public int? GlossaryId { get; set; }
        public virtual Glossary Glossary { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
