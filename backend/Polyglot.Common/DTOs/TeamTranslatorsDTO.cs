using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class TeamTranslatorsDTO
    {
        public int TeamId { get; set; }
        public List<int> TranslatorIds { get; set; }
    }
}
