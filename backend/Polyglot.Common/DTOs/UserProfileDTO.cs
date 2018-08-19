using System;
using System.Collections.Generic;
using Polyglot.DataAccess.Entities;
using static Polyglot.DataAccess.Entities.UserProfile;

namespace Polyglot.Common.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
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

        public ICollection<RatingDTO> Ratings { get; set; }
        public ICollection<TeammateDTO> TeamTranslators { get; set; }
        public List<ProjectDTO> Projects { get; set; }

        public UserProfileDTO()
        {
            TeamTranslators = new List<TeammateDTO>();
            Ratings = new List<RatingDTO>();
            Projects = new List<ProjectDTO>();
        }
    }
}

