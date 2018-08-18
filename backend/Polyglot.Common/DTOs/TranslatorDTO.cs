using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TranslatorDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public IEnumerable<RightDTO> Rights { get; set; }

        public double Rating { get; set; }

        public int TeamId { get; set; }

        public TranslatorDTO()
        {
            Rights = new List<RightDTO>();
        }
    }
}
