using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class TeamTranslator : Entity
    {
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int? TranslatorId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<TranslatorRight> TranslatorRights { get; set; }

        public TeamTranslator()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
