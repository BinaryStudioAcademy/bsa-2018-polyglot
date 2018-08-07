using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Translator : Entity
    {
        public UserProfile UserProfile { get; set; }
		
        public List<Rating> Ratings { get; set; }
        public List<TeamTranslator> TeamTranslators { get; set; }

        public Translator()
        {
            TeamTranslators = new List<TeamTranslator>();
        }
    }
}
