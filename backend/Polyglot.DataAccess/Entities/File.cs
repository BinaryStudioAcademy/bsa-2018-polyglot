using System;

namespace Polyglot.DataAccess.Entities
{
    public class File : Entity
    {
        public string Link { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserProfile UploadedBy { get; set; }
        public Project Project { get; set; }
    }
}
