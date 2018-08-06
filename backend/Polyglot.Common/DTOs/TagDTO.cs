using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }

        public List<ProjectTagDTO> ProjectTags { get; set; } 

        public TagDTO()
        {
            ProjectTags = new List<ProjectTagDTO>();
        }
    }
}
