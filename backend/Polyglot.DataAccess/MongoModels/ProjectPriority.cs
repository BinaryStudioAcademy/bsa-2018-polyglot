using Polyglot.DataAccess.Entities;
using System.Collections.Generic;

namespace Polyglot.DataAccess.MongoModels
{
    public class ProjectPriority : Entity
    {
        public int UserId { get; set; }

        public int Total { get; set; }

        public List<Priority> Priorities { get; set; }

        public ProjectPriority()
        {
            this.Priorities = new List<Priority>();
        }
    }
}
