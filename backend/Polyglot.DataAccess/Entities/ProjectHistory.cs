using System;

namespace Polyglot.DataAccess.Entities
{
    public class ProjectHistory : Entity
    {
        public Project Project { get; set; }
        public UserProfile Actor { get; set; }
        public string TableName { get; set; }
        public string ActionType { get; set; }
        public string OriginValue { get; set; }
        public DateTime Time { get; set; }
    }
}
