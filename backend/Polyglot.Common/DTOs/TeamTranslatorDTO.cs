using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TeamTranslatorDTO
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public TeamDTO Team { get; set; }

        public int TranslatorId { get; set; }
        public TranslatorDTO Translator { get; set; }

        public List<TranslatorRightDTO> TranslatorRights { get; set; }

        public TeamTranslatorDTO()
        {
            TranslatorRights = new List<TranslatorRightDTO>();
        }
    }
}
