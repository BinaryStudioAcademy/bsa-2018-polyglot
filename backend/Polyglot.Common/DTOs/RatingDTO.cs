using System;

namespace Polyglot.Common.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public UserProfileDTO CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
