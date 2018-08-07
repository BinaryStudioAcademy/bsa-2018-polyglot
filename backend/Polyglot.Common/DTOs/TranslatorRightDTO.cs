namespace Polyglot.Common.DTOs
{
    public class TranslatorRightDTO
    {
        public int Id { get; set; }
        public int TeamTranslatorId { get; set; }
        public TeamTranslatorDTO TeamTranslator { get; set; }

        public int RightId { get; set; }
        public RightDTO Right { get; set; }
    }
}
