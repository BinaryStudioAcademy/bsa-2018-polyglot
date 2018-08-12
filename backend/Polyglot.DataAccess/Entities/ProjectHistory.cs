using System;

namespace Polyglot.DataAccess.Entities
{
    public class ProjectHistory : Entity
    {
        public virtual Project Project { get; set; }
        public virtual UserProfile Actor { get; set; }
        public string TableName { get; set; }
        public string ActionType { get; set; }
        public string OriginValue { get; set; }
        public virtual DateTime Time { get; set; }
    }
}
