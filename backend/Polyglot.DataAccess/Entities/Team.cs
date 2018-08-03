using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public List<TeamTranslator> TeamTranslators { get; set; }

        public Team()
        {
            TeamTranslators = new List<TeamTranslator>();
        }    
    }
}
