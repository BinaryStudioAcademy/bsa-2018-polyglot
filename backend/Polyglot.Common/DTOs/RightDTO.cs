using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class RightDTO
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public List<RightDTO> TranslatorRights { get; set; }

        public RightDTO()
        {
            TranslatorRights = new List<RightDTO>();
        }
    }
}
