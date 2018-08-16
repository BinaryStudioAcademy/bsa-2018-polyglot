using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class GlossaryDTO
    {
        public int Id { get; set; }
        public string TermText { get; set; }
        public string ExplanationText { get; set; }
        public string OriginLanguage { get; set; }
        
        public List<GlossaryDTO> ProjectGlossaries { get; set; }

        public GlossaryDTO()
        {
            ProjectGlossaries = new List<GlossaryDTO>();
        }
    }
}
