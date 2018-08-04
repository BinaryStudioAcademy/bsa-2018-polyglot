using System;

namespace Polyglot.DataAccess.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public UserProfile CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
