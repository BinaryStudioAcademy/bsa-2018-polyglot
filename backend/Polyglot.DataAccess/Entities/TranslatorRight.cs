namespace Polyglot.DataAccess.Entities
{
    public class TranslatorRight : MidEntity
    {        
        public int? TeamTranslatorId { get; set; }
        public virtual TeamTranslator TeamTranslator { get; set; }

        public int? RightId { get; set; }
        public virtual Right Right { get; set; }
    
    }
}
