namespace Polyglot.DataAccess.Entities
{
    public class TranslatorRight : Entity
    {
        public int TeamTranslatorId { get; set; }
        public TeamTranslator TeamTranslator { get; set; }

        public int RightId { get; set; }
        public Right Right { get; set; }
    }
}
