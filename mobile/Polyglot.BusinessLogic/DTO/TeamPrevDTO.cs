using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class TeamPrevDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserProfileDTO CreatedBy { get; set; }

        public int ListHeight { get; set; }

        public List<UserProfilePrevDTO> Persons { get; set; }

        public TeamPrevDTO()
        {
            Persons = new List<UserProfilePrevDTO>();
        }
    }
}
