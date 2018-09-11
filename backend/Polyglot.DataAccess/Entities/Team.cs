using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public virtual UserProfile CreatedBy { get; set; }

        public virtual ICollection<TeamTranslator> TeamTranslators { get; set; }

        public virtual ICollection<ProjectTeam> ProjectTeams { get; set; }

        public Team()
        {
            ProjectTeams = new List<ProjectTeam>();
            TeamTranslators = new List<TeamTranslator>();
        }    
    }
}
