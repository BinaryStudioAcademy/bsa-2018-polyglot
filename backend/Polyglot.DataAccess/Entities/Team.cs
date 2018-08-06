using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Team : Entity
    {
        public List<TeamTranslator> TeamTranslators { get; set; }

        public Team()
        {
            TeamTranslators = new List<TeamTranslator>();
        }    
    }
}
