using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Right : Entity
    {
        public string Definition { get; set; }
        public virtual ICollection<TranslatorRight> TranslatorRights { get; set; }

        public Right()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
