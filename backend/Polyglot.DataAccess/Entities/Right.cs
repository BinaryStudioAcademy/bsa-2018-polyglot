using Polyglot.DataAccess.Helpers;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Right : Entity
    {
        public RightDefinition Definition { get; set; }
        public virtual ICollection<TranslatorRight> TranslatorRights { get; set; }

        public Right()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
