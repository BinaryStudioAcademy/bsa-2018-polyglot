using Polyglot.DataAccess.Helpers;
using System;

namespace Polyglot.Common.DTOs.Chat
{
    public class ChatUserDTO
    {
        public int Id { get; set; }

        public string Uid { get; set; }

        public int Hash { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public Role Role { get; set; }

        public DateTime LastSeen { get; set; }

        public bool IsOnline { get; set; }

        public override int GetHashCode()
        {
            var hash = 13;
            return hash * 25 + this.Id;
        }
    }
}
