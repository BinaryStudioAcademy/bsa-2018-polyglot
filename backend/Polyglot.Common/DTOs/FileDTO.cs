using System;

namespace Polyglot.Common.DTOs
{
    public class FileDTO
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserProfileDTO UploadedBy { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
