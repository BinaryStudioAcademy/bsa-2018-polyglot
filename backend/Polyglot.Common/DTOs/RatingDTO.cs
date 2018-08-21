using System;

namespace Polyglot.Common.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual UserProfileDTO User { get; set; }
        public int CreatedById { get; set; }
        public virtual UserProfileDTO CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
