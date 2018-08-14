namespace Polyglot.DataAccess.Entities
{
    public class ComplexString : Entity
    {
        public string TranslationKey { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
