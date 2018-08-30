using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class ProjectTeam : Entity
    {
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
