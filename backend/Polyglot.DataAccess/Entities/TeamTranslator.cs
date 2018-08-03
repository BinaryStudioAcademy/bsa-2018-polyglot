using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class TeamTranslator
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TranslatorId { get; set; }
        public Translator Translator { get; set; }
    }
}
