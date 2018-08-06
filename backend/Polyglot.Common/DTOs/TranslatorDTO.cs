using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TranslatorDTO
    {
        public int Id { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public RatingDTO Rating { get; set; }

        public List<TeamTranslatorDTO> TeamTranslators { get; set; }

        public TranslatorDTO()
        {
            TeamTranslators = new List<TeamTranslatorDTO>();
        }
    }
}
