using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.BusinessLogic.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Technology { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ProjectDTO()
        {
        }
    }
}
