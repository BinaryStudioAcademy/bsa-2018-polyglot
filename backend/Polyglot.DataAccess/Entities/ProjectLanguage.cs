namespace Polyglot.DataAccess.Entities
{
    public class ProjectLanguage : MidEntity
    {
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
