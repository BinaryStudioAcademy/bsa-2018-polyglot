using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Right
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public List<TranslatorRight> TranslatorRights { get; set; }

        public Right()
        {
            TranslatorRights = new List<TranslatorRight>();
        }
    }
}
