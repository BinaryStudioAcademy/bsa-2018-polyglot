using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TranslatorDTO
    {
        public int Id { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public List<RatingDTO> Ratings { get; set; }

        public List<TranslatorDTO> TeamTranslators { get; set; }

        public TranslatorDTO()
        {
            TeamTranslators = new List<TranslatorDTO>();
        }
    }
}
