using System;

namespace Polyglot.DataAccess.Entities
{
    public class Rating : Entity
    {
        public double Rate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
        public int CreatedById { get; set; }
        public virtual UserProfile CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
