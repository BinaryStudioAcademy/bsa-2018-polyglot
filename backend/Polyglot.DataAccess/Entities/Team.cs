using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public UserProfile CreatedBy { get; set; }

        public virtual ICollection<TeamTranslator> TeamTranslators { get; set; }

        public Team()
        {
            TeamTranslators = new List<TeamTranslator>();
        }    
    }
}
