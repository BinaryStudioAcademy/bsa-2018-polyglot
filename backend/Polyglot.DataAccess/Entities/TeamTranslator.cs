using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class TeamTranslator
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TranslatorId { get; set; }
        public Translator Translator { get; set; }

        public List<TranslatorRight> TranslatorRights { get; set; }

        public TeamTranslator()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
