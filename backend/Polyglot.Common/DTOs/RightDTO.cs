using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class RightDTO
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public List<TranslatorRightDTO> TranslatorRights { get; set; }

        public RightDTO()
        {
            TranslatorRights = new List<TranslatorRightDTO>();
        }
    }
}
