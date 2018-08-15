using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TeammateDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public IEnumerable<RightDTO> Rights { get; set; }

        public int TeamId { get; set; }

        public TeammateDTO()
        {
            Rights = new List<RightDTO>();
        }
    }
}
