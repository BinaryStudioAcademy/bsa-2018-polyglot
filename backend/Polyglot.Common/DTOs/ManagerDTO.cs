using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class ManagerDTO
    {
        public int Id { get; set; }
        public UserProfileDTO UserProfile { get; set; }

        public List<ProjectDTO> Projects { get; set; }
    }
}