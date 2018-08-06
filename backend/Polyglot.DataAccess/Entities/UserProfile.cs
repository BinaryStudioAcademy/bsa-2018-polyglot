using System;

namespace Polyglot.DataAccess.Entities
{
    public class UserProfile
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
    }
}

