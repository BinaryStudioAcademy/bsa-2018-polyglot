using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class Manager : Entity
    {
        public virtual UserProfile UserProfile { get; set; }
		
		public virtual List<Project> Projects { get; set; }
    }
}