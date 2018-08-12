using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Tag : Entity
    {
        public string Color { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectTag> ProjectTags { get; set; } 

        public Tag()
        {
            ProjectTags = new List<ProjectTag>();
        }
    }
}
