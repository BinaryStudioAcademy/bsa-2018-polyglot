﻿using System;
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
        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }

        public int UserRole { get; set; }

        public ICollection<RatingDTO> Ratings { get; set; }
        public ICollection<TranslatorDTO> TeamTranslators { get; set; }
        public ICollection<NotificationDTO> Notifications { get; set; }
        public List<ProjectDTO> Projects { get; set; }

        public UserProfileDTO()
        {
            TeamTranslators = new List<TranslatorDTO>();
            Ratings = new List<RatingDTO>();
            Projects = new List<ProjectDTO>();
            Notifications = new List<NotificationDTO>();
        }
    }
}

