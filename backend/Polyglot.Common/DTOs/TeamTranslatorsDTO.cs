using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.Common.DTOs
{
    public class TeamTranslatorsDTO
    {
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public List<int> TranslatorIds { get; set; }
    }
}
