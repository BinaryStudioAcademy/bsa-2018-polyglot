using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class GlossaryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GlossaryStringDTO> GlossaryStrings { get; set; }

        public LanguageDTO OriginLanguage { get; set; }

        public UserProfileDTO UserProfile { get; set; }

        public List<ProjectDTO> ProjectGlossaries { get; set; }

        public GlossaryDTO()
        {
            GlossaryStrings = new List<GlossaryStringDTO>();
            ProjectGlossaries = new List<ProjectDTO>();
        }
    }
}
