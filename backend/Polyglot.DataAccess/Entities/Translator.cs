using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Translator : Entity
    {
        public virtual UserProfile UserProfile { get; set; }
		
        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<TeamTranslator> TeamTranslators { get; set; }

        public Translator()
        {
            TeamTranslators = new List<TeamTranslator>();
            Ratings = new List<Rating>();
        }
    }
}
