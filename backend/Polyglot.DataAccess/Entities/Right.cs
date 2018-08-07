using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Right : Entity
    {
        public string Definition { get; set; }
        public List<TranslatorRight> TranslatorRights { get; set; }

        public Right()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
