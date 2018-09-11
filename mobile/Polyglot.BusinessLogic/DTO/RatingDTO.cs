using System;

namespace Polyglot.BusinessLogic.DTO
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public int CreatedById { get; set; }
        public virtual UserDTO CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
