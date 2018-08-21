using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }

#warning нужно добавить manager-a

        public List<TranslatorDTO> TeamTranslators { get; set; }

        public TeamDTO()
        {
            TeamTranslators = new List<TranslatorDTO>();
        }    
    }
}
