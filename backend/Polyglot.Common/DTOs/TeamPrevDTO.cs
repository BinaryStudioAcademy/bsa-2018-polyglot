using System.Collections.Generic;

namespace Polyglot.Common.DTOs
{
    public class TeamPrevDTO
    {
        public int Id { get; set; }
        public List<UserProfilePrevDTO> Persons { get; set; }

        public TeamPrevDTO()
        {
            Persons = new List<UserProfilePrevDTO>();
        }
    }
}
