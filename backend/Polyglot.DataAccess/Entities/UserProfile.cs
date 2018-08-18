using System;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities
{
    public class UserProfile : Entity
    {
        public string FullName { get; set; }

        public string Uid { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string AvatarUrl { get; set; }

        public Role UserRole { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<TeamTranslator> TeamTranslators { get; set; }

        public virtual List<Project> Projects { get; set; }

        public UserProfile()
        {
            TeamTranslators = new List<TeamTranslator>();
            Ratings = new List<Rating>();
            Projects = new List<Project>();
        }

        public enum Role { Manager, Translator }

    }
}

