using System;

namespace Polyglot.DataAccess.Entities
{
    public class File : Entity
    {
        public string Link { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual UserProfile UploadedBy { get; set; }
        public virtual Project Project { get; set; }
    }
}
